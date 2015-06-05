using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using MainClass;
using System.Diagnostics;
using CommonPlugin;
using Newtonsoft.Json;
using IConverter_XML;

namespace Films
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        object current;
        //public List<object> myList = new List<object>();
        public Boolean inputFlag = false, isNewClass = false;
        public Type[] extraTypes, types, adapter;
        public string currentPath;
        public Assembly SampleAssembly;
        private PluginManager pm = new PluginManager();
        // создаем объект указанного типа
        public object GetInstance(string strFullyQualifiedName)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return Activator.CreateInstance(t);
        }
        // получаем неполное имя класса
        private string getName(string fullName)
        {
            return fullName.Substring(fullName.IndexOf(".") + 1, fullName.Length - fullName.IndexOf(".") - 1);
        }
        // добавление нового объекта
        private void Add_Click(object sender, EventArgs e)
        {
            string typeOfClass = "";
            // если объект, который мы хотим создать имеет тип прописанный в программе
            if (listOfClasses.SelectedIndex < 3)
            {
                typeOfClass = "Films." + listOfClasses.SelectedItem.ToString();
                current = GetInstance(typeOfClass);
            }
            // тип содаваемого объекта новый(полученный из dll)
            else
            {
                current = Activator.CreateInstance(types[listOfClasses.SelectedIndex - 3]);
            }
            //myList.Add(current);
            Singleton.getInstance().add(current);
            listCreateObjects.Items.Add(getName(current.GetType().ToString()));
            
        }
        // удаление объекта
        private void delete_Click(object sender, EventArgs e)
        {
            if (listCreateObjects.SelectedIndex != -1)
            {
                //myList.RemoveAt(listCreateObjects.SelectedIndex);
                Singleton.getInstance().deleteByIndex(listCreateObjects.SelectedIndex);
                listCreateObjects.Items.RemoveAt(listCreateObjects.SelectedIndex);
                informationList.Items.Clear();
            }
        }
        // используется для отображения на экран свойств выбранного объекта
        private void informationAboutObject(Type myType, System.Reflection.PropertyInfo[] propertyInfo)
        {
            informationList.Items.Clear();
            informationList.Items.Add(getName(myType.ToString()));
            foreach (System.Reflection.PropertyInfo info in propertyInfo)
            {
                informationList.Items.Add(info.Name + ": " + info.GetValue(current).ToString());
            }
        }
        // получение свойств об объекте
        private void listCreateObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listCreateObjects.SelectedIndex == -1)
                return;
            else
            {
                //current = myList[listCreateObjects.SelectedIndex];
                current = Singleton.getInstance().getByIndex(listCreateObjects.SelectedIndex);
                Type myType = current.GetType();
                System.Reflection.PropertyInfo[] propertyInfo = myType.GetProperties();
                informationAboutObject(myType, propertyInfo);
            }
        }
        //изменение свойств объекта. Если нажата кнопка, то записывается новое значение
        private void changeButton_Click(object sender, EventArgs e)
        {
            inputFlag = true;
        }
        // редактирование свойств объекта
        private void edit_Click(object sender, EventArgs e)
        {
            informationList.Items.Clear();
            editionField.Text = "";
            if (listCreateObjects.SelectedIndex == -1)
                return;
            //current = myList[listCreateObjects.SelectedIndex];
            current = Singleton.getInstance().getByIndex(listCreateObjects.SelectedIndex);
            Type myType = current.GetType();
            System.Reflection.PropertyInfo[] propertyInfo = myType.GetProperties();
            informationAboutObject(myType, propertyInfo);
            foreach (System.Reflection.PropertyInfo info in propertyInfo )
            {
                editionField.Text = editionField.Text + "Вставьте '" + info.Name + "'" + "\n";
                inputFlag = false;
                while (!inputFlag) 
                {
                    Application.DoEvents();
                }
                if (info.GetValue(current) is int)
                {
                    int newValue;
                    if (Int32.TryParse(textValue.Text, out newValue))
                    {
                        info.SetValue(current, newValue);
                        editionField.Text = editionField.Text + info.Name + " = " + textValue.Text + "\n";
                    }
                    else
                    {
                        MessageBox.Show("Недопустимое значение","Error");
                        editionField.Text = editionField.Text + info.Name + " = " + info.GetValue(current) + "\n";
                    }
                }
                else
                {
                    if (textValue.Text == "")
                        editionField.Text = editionField.Text + info.Name + " = " + info.GetValue(current) + "\n";
                    else
                    {
                        info.SetValue(current, textValue.Text);
                        editionField.Text = editionField.Text + info.Name + " = " + textValue.Text + "\n";
                    }
                }
                textValue.Text = "";
            }
            informationAboutObject(myType, propertyInfo);
            MessageBox.Show("Объект редактирован успешно!","Message");
        }
        // очищаем список от объектов
        private void cleanMyList_Click(object sender, EventArgs e)
        {
            listCreateObjects.Items.Clear();
            informationList.Items.Clear();
            editionField.Text = "";
            //myList.Clear();
            Singleton.getInstance().clearMyList();
        }
        // сериализация
        private void serialization_Click(object sender, EventArgs e)
        {
            informationList.Items.Clear();
            editionField.Text = "";
            //Facade.OperationSerialize(myList, extraTypes, types, isNewClass);
            Facade.OperationSerialize(Singleton.getInstance().getMyList(), extraTypes, types, isNewClass);
            //getTypesArray();
            /*XmlSerializer xs = new XmlSerializer(typeof(MyListCollection), extraTypes);

            var myCollection = new MyListCollection();
            myCollection.myList = myList;
            TextWriter writer = new StreamWriter("file.xml");
            xs.Serialize(writer, myCollection);
            writer.Close();*/
            MessageBox.Show("Сериализация прошла успешно","Message");
        }
        // срздаем массив типов наших объектов
       /* private void getTypesArray()
        {
            if (isNewClass == true)
            {
                extraTypes = new Type[types.Length + 3];
                int count = 3;
                foreach (Type tempType in types)
                {
                    extraTypes[count] = tempType;
                    ++count;
                }
            }
            else
            {
                extraTypes = new Type[3];
            }
            extraTypes[0] = typeof(Cartoons);
            extraTypes[1] = typeof(Fiction);
            extraTypes[2] = typeof(Melodrama);
        }*/
        // десериализация
        private void deserialization_Click(object sender, EventArgs e)
        {
            informationList.Items.Clear();
            editionField.Text = "";
            listCreateObjects.Items.Clear();
            //myList.Clear();
            //myList = Facade.OperationDeserialize(extraTypes, types, isNewClass);
            Singleton.getInstance().clearMyList();
            Singleton.getInstance().setMyList(Facade.OperationDeserialize(extraTypes, types, isNewClass));
            //getTypesArray();
            /*XmlSerializer mySerializer = new XmlSerializer(typeof(MyListCollection), extraTypes);
            FileStream fs = new FileStream("file.xml", FileMode.Open);
            MyListCollection myCollection = (MyListCollection)mySerializer.Deserialize(fs);
            myList = myCollection.myList;*/
            //foreach (object obj in myList)
            foreach (object obj in Singleton.getInstance().getMyList())
                listCreateObjects.Items.Add(getName(obj.ToString()));
            MessageBox.Show("Десериализация прошла успешно", "Message");
        }
        // выбираем dll-ку, которую хоти загрузить ( в textBox1.Text записывается путь)
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                currentPath = textBox1.Text;
                string buf = currentPath.Substring(currentPath.IndexOf(".") + 1, currentPath.Length - currentPath.IndexOf(".") - 1);
                if (buf != "dll")
                {
                    currentPath = "";
                    textBox1.Text = "";
                    MessageBox.Show("Выберите файл расширением dll","Error");
                    return;
                }
            }
            
        }
        // добавление нового класса в программу
        private void AddClass_Click(object sender, EventArgs e)
        {
            isNewClass = false;
            if (currentPath == "")
                MessageBox.Show("Выберите файл!!!","Error");
            else 
            {
                SampleAssembly = Assembly.LoadFrom(currentPath);
                try
                { 
                    types = SampleAssembly.GetTypes(); 
                }
                // в случае ошибки при загрузке dll
                catch
                {
                    MessageBox.Show("Не удалось загрузить библиотеку","Error");
                    currentPath = "";
                    return;
                }
                foreach (Type myType in types)
                {
                    // плагин должен иметь основной тип - Film, иначе, он не будет загружен
                    if (myType.BaseType == typeof(Film))
                    {
                        listOfClasses.Items.Add(getName(myType.ToString()));
                        isNewClass = true;
                    }
                }
                if (isNewClass)
                    MessageBox.Show("Класс добавлен","Message");
                else
                    MessageBox.Show("dll не соответсвует формату","Error");
                textBox1.Text = "";
                currentPath = "";
            }
        }

        private void AddPlugin_Click(object sender, EventArgs e)
        {
            comboBoxPlugin.Text = "";
            pm.ScanPlugins(AppDomain.CurrentDomain.BaseDirectory + "Plugins\\");

            foreach (var plugin in pm.Plugins)
            {
                string pluginName = plugin.pluginName;
                comboBoxPlugin.Items.Add(pluginName);
            }
            //comboBoxPlugin.Items.Clear();
            MessageBox.Show("Плагины загружены");
        }

        private void comboBoxPlugin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPlugin.SelectedIndex == -1)
                return;
            int number = comboBoxPlugin.SelectedIndex;
            try
            {
                pm.Plugins[number].PluginFunction();
                MessageBox.Show("Преобразование прошло успешно", "Message");
            }
            catch
            {
                MessageBox.Show("Ошибка!!! Возможно, вы пытаетесь загрузить поврежденный файл", "Error");
            }
        }

        private void AddForeignPlugin_Click(object sender, EventArgs e)
        {
            comboBoxForeignPlugin.Text = "";
            try
            {
                SampleAssembly = Assembly.LoadFrom("Plugins//XML_Converter.dll");
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить сторонний плагин");
                return;
            }
            adapter = SampleAssembly.GetTypes();
            foreach (Type t in adapter)
            {
                try 
                {
                    PluginAdapter Adapter = new PluginAdapter();
                    Adapter.SetPlugin = (ITransform)Activator.CreateInstance(t);
                    comboBoxForeignPlugin.Items.Add(Adapter.pluginName);
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при адаптировании нового плагина","Error");
                    return;
                }
            }
            MessageBox.Show("Плагин добавлен успешно");            
        }

        private void comboBoxForeignPlugin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxForeignPlugin.SelectedIndex == -1)
                return;
            int newCount = comboBoxForeignPlugin.SelectedIndex;
            try
            {
                PluginAdapter Adapter = new PluginAdapter();
                Adapter.SetPlugin = (ITransform)Activator.CreateInstance(adapter[newCount]);
                Adapter.PluginFunction();
                MessageBox.Show("Преобразование прошло успешно","Message");
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при трансформации! Возможно ваш файл поврежден","Error");
            }

        }
        
    }
}
