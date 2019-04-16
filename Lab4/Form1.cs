using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Http;

namespace Lab4
{
    public partial class Form1 : Form
    {
        struct selected_gun_struct
        {
            public dynamic obj;
            public Type type;
        }
        selected_gun_struct selected_gun; 
        List<dynamic> guns = new List<dynamic>();
        List<Type> gun_types = new List<Type>();
        List<dynamic> serialization_list = new List<dynamic>();
        List<Type> subclasses = new List<Type>();
        int offset = 20;
        const int margin = 40;
        public Form1()
        {
            Console.WriteLine(GunType.TypeList);
            selected_gun = new selected_gun_struct();
            InitializeComponent();
            comboBox2.DisplayMember = "Name";
            Assembly a = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == "Lab4");
            foreach (Type type in a.GetTypes())
            {
                if(type.IsSubclassOf(typeof(Gun)))
                {
                    subclasses.Add(type);
                }
            }
            //Assembly asm = Assembly.LoadFrom("Nade.dll");
            //Type[] TGuns = asm.GetTypes();
            //subclasses.AddRange(TGuns);
            FillTypeComboBox();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            offset = 20;
            if (comboBox1.SelectedItem is GunType) {
                string selectedItem = (comboBox1.SelectedItem as GunType).Type;
                PropertyInfo[] properties = Type.GetType("Lab4."+selectedItem).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    AddField(property, panel1, 1);
                }
            }
            else
            {
                return;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TypeInfo selected_type = (comboBox1.SelectedItem as TypeInfo);
            List<object> input = new List<object>();
            TextBox[] textBoxes = panel1.Controls.OfType<TextBox>().ToArray();
            for (int i=0; i<selected_type.parameters.Count(); i++)
            {
                input.Add(Convert.ChangeType(textBoxes[i].Text, selected_type.parameters[i].ParameterType));
            }
            object[] args = input.ToArray();
            dynamic new_gun = Activator.CreateInstance(selected_type.type, args);
            guns.Add(new_gun);
            UpdateGunList();
            foreach (Control obj in panel1.Controls)
            {
                if (obj is TextBox)
                {
                    obj.ResetText();
                }
            }
            UpdateInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selected_gun.obj.Shoot();
            UpdateInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selected_gun.type.GetMethod("Reload") != null)
            {
                selected_gun.obj.Reload();
                UpdateInfo();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            foreach(PropertyInfo property in selected_gun.type.GetProperties())
            {
                string txt = panel2.Controls[GetTextBoxName(TransformPropName(property.Name), 2)].Text;
                property.SetValue(selected_gun.obj, Convert.ChangeType(txt ,property.PropertyType));
            }
            UpdateInfo();
            UpdateGunList();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int idx = serialization_list.IndexOf(selected_gun.obj); //index of object in @serialization_list
            if (idx == -1) //check if object already in @serialization_list
            {
                serialization_list.Add(selected_gun.obj);
                textBox1.Text += selected_gun.type.GetProperty("name").GetValue(selected_gun.obj) + Environment.NewLine;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("Guns.dat", FileMode.OpenOrCreate);
            formatter.Serialize(fs, serialization_list.ToArray());
            fs.Close();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("Guns.dat", FileMode.Open);
            dynamic[] loaded_guns = (dynamic[])formatter.Deserialize(fs);
            foreach (dynamic loaded_gun in loaded_guns)
            {
                guns.Add(loaded_gun);
            }
            UpdateGunList();
            fs.Close();
        }
        public void UpdateInfo()
        {
            selected_gun.type = Type.GetType(comboBox2.SelectedItem.GetType().AssemblyQualifiedName);
            selected_gun.obj = Convert.ChangeType(comboBox2.SelectedItem, selected_gun.type); ;
            panel2.Controls.Clear();
            offset = 20;
            PropertyInfo[] properties = selected_gun.obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                AddField(property, panel2, 2, property.GetValue(selected_gun.obj).ToString());
            }
        }
        public void UpdateGunList()
        {
            comboBox2.Items.Clear();
            foreach (dynamic gun in guns)
            {
                comboBox2.Items.Add(gun);
            }
            comboBox2.SelectedIndex = 0;
        }
        public class TypeInfo
        {
            public Type type;
            public ParameterInfo[] parameters;
            public TypeInfo() { }
        }
        public void AddField(PropertyInfo parameter, Panel parent, int id)
        {
            string name = TransformPropName(parameter.Name);
            TextBox txt = new TextBox();
            txt.Name = GetTextBoxName(name, id);
            txt.Height = 15;
            txt.Width = 150;
            txt.Top = offset;
            txt.Text = "1";
            parent.Controls.Add(txt);

            Label lbl = new Label();
            lbl.Text = name;
            lbl.Top = offset - 15;
            parent.Controls.Add(lbl);
            offset += margin;
        }
        public void AddField(PropertyInfo property, Panel parent, int id, string value)
        {
            string name = TransformPropName(property.Name);
            TextBox txt = new TextBox();
            txt.Name = GetTextBoxName(name, id);
            txt.Height = 15;
            txt.Width = 150;
            txt.Top = offset;
            txt.Text = value;
            txt.Enabled = property.SetMethod.IsPublic;
            parent.Controls.Add(txt);

            Label lbl = new Label();
            lbl.Text = name;
            lbl.Top = offset - 15;
            parent.Controls.Add(lbl);
            offset += margin;
        }
        public string TransformPropName(string prop)
        {
            return prop.First().ToString().ToUpper() + prop.Substring(1).Replace("_", " ");
        }
        public string GetTextBoxName(string prop_name, int id = 0)
        {
            return prop_name.Replace(" ", "") + "TextBox" + id.ToString();
        }
        public void FillTypeComboBox()
        {
            foreach(GunType gunType in GunType.TypeList)
            {
                comboBox1.Items.Add(gunType);
            }
            /*foreach (Type type in subclasses)
            {
                if (!type.IsAbstract && type.IsSerializable)
                {
                    TypeInfo new_type = new TypeInfo();
                    new_type.type = type;
                    ConstructorInfo[] constructors = type.GetConstructors();
                    foreach (ConstructorInfo constructor in constructors)
                    {
                        ParameterInfo[] parameters = constructor.GetParameters();
                        if (parameters.Count() > 0)
                        {
                            new_type.parameters = parameters;
                            break;
                        }
                    }
                    comboBox1.Items.Add(new_type);
                }
            }*/
            comboBox1.SelectedIndex = 0;
        }
    }
}
