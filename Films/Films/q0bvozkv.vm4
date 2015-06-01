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

namespace Films
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        object current;
        public List<object> myList = new List<object>();
        public Boolean inputFlag = false, isNewClass = false;
        public Type[] extraTypes, types;
        public string currentPath;
        public Assembly SampleAssembly;

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
            myList.Add(current);
            listCreateObjects.Items.Add(getName(current.GetType().ToString()));
            
        }
        // удаление объекта
        private void delete_Click(object sender, EventArgs e)
        {
            if (listCreateObjects.SelectedIndex != -1)
            {
                myList.RemoveAt(listCreateObjects.SelectedIndex);
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
                current = myList[listCreateObjects.SelectedIndex];
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
            current = myList[listCreateObjects.SelectedIndex];
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
                        MessageBox.Show("Invalid value");
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
        }
        // очищаем список от объектов
        private void cleanMyList_Click(object sender, EventArgs e)
        {
            listCreateObjects.Items.Clear();
            informationList.Items.Clear();
            editionField.Text = "";
            myList.Clear();
        }
        // сериализация
        private void serialization_Click(object sender, EventArgs e)
        {
            informationList.Items.Clear();
            editionField.Text = "";
            getTypesArray();
            XmlSerializer xs = new XmlSerializer(typeof(MyListCollection), extraTypes);

            var myCollection = new MyListCollection();
            myCollection.myList = myList;
            TextWriter writer = new StreamWriter("file.xml");
            xs.Serialize(writer, myCollection);
            writer.Close();
            MessageBox.Show("Сериализация прошла успешно");
        }
        // срздаем массив типов наших объектов
        private void getTypesArray()
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
        }
        // десериализация
        private void deserialization_Click(object sender, EventArgs e)
        {
            informationList.Items.Clear();
            editionField.Text = "";
            listCreateObjects.Items.Clear();
            myList.Clear();
            getTypesArray();
            XmlSerializer mySerializer = new XmlSerializer(typeof(MyListCollection), extraTypes);
            FileStream fs = new FileStream("file.xml", FileMode.Open);
            MyListCollection myCollection = (MyListCollection)mySerializer.Deserialize(fs);
            myList = myCollection.myList;
            foreach (object obj in myList)
                listCreateObjects.Items.Add(getName(obj.ToString()));
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
                    MessageBox.Show("Выберите файл расширением dll");
                    return;
                }
            }
            
        }
        // добавление нового класса в программу
        private void AddClass_Click(object sender, EventArgs e)
        {
            isNewClass = false;
            if (currentPath == "")
                MessageBox.Show("Выберите файл!!!");
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
                    MessageBox.Show("Не удалось загрузить библиотеку");
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
                    MessageBox.Show("Класс добавлен");
                else
                    MessageBox.Show("dll не соответсвует формату");
                textBox1.Text = "";
                currentPath = "";
            }
        }
        
    }
}
