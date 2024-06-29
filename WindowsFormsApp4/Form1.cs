using System;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Dish;
using WindowsFormsApp4.Properties;
using System.Media;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Menu_Appetizers menu_appetizers = new Menu_Appetizers();
        Menu_Desserts menu_desserts= new Menu_Desserts();
        Menu_Vegan menu_vegans = new Menu_Vegan();
        Menu_Meat menu_meats = new Menu_Meat(); 
        Menu_Dairy menu_dairy = new Menu_Dairy(); 
        The_menu the_menu = new The_menu();

        string UsedFile;
        private void create_new_menu_Click(object sender, EventArgs e)
        {
            if (UsedFile == null)
            {
                the_menu.appetizers = menu_appetizers;
                the_menu.desserts = menu_desserts;
                the_menu.vegans = menu_vegans;
                the_menu.meats = menu_meats;
                the_menu.dairy = menu_dairy;

                start_bar.Visible = true;
                timer1.Start();
            }
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream = File.Open(openFileDialog1.FileName, FileMode.Open);
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                the_menu = (The_menu)binaryFormatter.Deserialize(stream);
                pictureBox1.Invalidate();
                UsedFile = openFileDialog1.FileName;
                stream.Close();

                start_bar.Visible = true;
                start_bar.Value = 0;
                timer1.Enabled = true;
            }
            else if (UsedFile == null)
            {
                MessageBox.Show("Load menu to continue!!!");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            start_bar.Increment(2);
            if (start_bar.Value == start_bar.Maximum)
            {
                timer1.Stop();
                start_panel.Visible = false;
                if (UsedFile == null)
                {
                    create_menu.Visible = true;
                    SoundPlayer music = new SoundPlayer(Properties.Resources.music);
                    music.PlayLooping();
                }
                else
                {
                    main_screen.Visible = true;
                    SoundPlayer music = new SoundPlayer(Properties.Resources.music);
                    music.PlayLooping();
                }
            }
            else
            {
                precent.Text = start_bar.Value.ToString()+" %";
            }
        }

        private void logInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsedFile == null)
            {
                MessageBox.Show("Load menu to continue or create a new menu!!!");
            }
            else
            {
                login.Visible = true;
                The_total_menu.Visible = false;
                main_screen.Visible = false;
                create_menu.Visible = false;
                recommendations_panel.Visible = false;
                start_panel.Visible = false;
                Show_dishes_panel.Visible = false;
            }
            
        }
        private void button_login_Click(object sender, EventArgs e)
        {
            if ((string.Equals(fill_user_name.Text, "Niva")) && string.Equals(textBox1.Text, "q") && robot.Checked == true)
            {
                not_manager.Visible = false;
                create_menu.Visible = true;
                login.Visible = false;
                fill_user_name.Text = "";
                textBox1.Text = "";
                robot.Checked = false;
            }
            else
            {
                not_manager.Visible = true;
                create_menu.Visible = false;
            }
        }

        private void Add_dish_CheckedChanged(object sender, EventArgs e)
        {
            if (Add_dish.Checked == true) 
            {
                add_dish_panel.Visible = true;

                Delete_dish.Checked = false;
                delete_select_main_type.Text = string.Copy("Select main type");
                delete_select_dish_type.Text = string.Copy("Select dish type");
                delete_list.Items.Clear();

                Change_dish.Checked = false;
                change_select_main_type.Text = string.Copy("Select main type");
                change_select_dish_type.Text = string.Copy("Select dish type");
                change_list.Items.Clear();
                change_select_main_type.Visible = false;
                change_one_dish.Visible = false;
            }
            else
            {
                add_dish_panel.Visible = false;
            }
        }

        private void Delete_dish_CheckedChanged(object sender, EventArgs e)
        {
            if (Delete_dish.Checked == true) 
            { 
                delete_dish_panel.Visible = true;

                input_name.Text = string.Empty;
                input_price.Text = string.Empty;
                allergenic_ingredients_input.Text = string.Empty;
                add_dish_panel.Visible = false;
                Add_dish.Checked = false;
                main_type_.Text = string.Copy("Select main type");
                dish_type_.Text = string.Copy("Select dish type");

                Change_dish.Checked = false;
                change_select_main_type.Text = string.Copy("Select main type");
                change_select_dish_type.Text = string.Copy("Select dish type");
                change_list.Items.Clear();
                change_select_main_type.Visible = false;
                change_one_dish.Visible = false;
            }
            else
            {
                delete_dish_panel.Visible = false;
            }
        }

        private void Change_dish_CheckedChanged(object sender, EventArgs e)
        {
            if (Change_dish.Checked == true) 
            { 
                change_dish_panel.Visible = true;

                input_name.Text = string.Empty;
                input_price.Text = string.Empty;
                allergenic_ingredients_input.Text = string.Empty;
                add_dish_panel.Visible = false;
                Add_dish.Checked = false;
                main_type_.Text = string.Copy("Select main type");
                dish_type_.Text = string.Copy("Select dish type");

                Delete_dish.Checked = false;
                delete_select_main_type.Text = string.Copy("Select main type");
                delete_select_dish_type.Text = string.Copy("Select dish type");
                delete_list.Items.Clear();
            }
            else { change_dish_panel.Visible = false; }
        }

        private void dish_type__SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dish_type_.Text.Equals("Mains"))
            {
                main_type_.Visible = true;
                description.Visible = true;
                dish_description_input.Visible = true;
            }
            else
            {
                main_type_.Visible = false;
                description.Visible = false;
                dish_description_input.Visible = false;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream = File.Open(openFileDialog1.FileName, FileMode.Open);
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                the_menu = (The_menu)binaryFormatter.Deserialize(stream);
                pictureBox1.Invalidate();
                UsedFile = openFileDialog1.FileName;
                stream.Close();
            }
            if (UsedFile == null)
            {
                the_menu.appetizers = menu_appetizers;
                the_menu.desserts = menu_desserts;
            }
        }

        private void save_new_menu_Click(object sender, EventArgs e)
        {
            int index;
           
            if (dish_type_.Text.Equals("Appetizers"))
            {
                index = the_menu.appetizers.NextIndexAppetizers;

                Appetizers_dishes a1 = new Appetizers_dishes(input_name.Text, input_price.Text, allergenic_ingredients_input.Text);

                the_menu.appetizers[index] = new Appetizers_dishes();
                the_menu.appetizers[index] = a1;
            }
            else if (dish_type_.Text.Equals("Desserts"))
            {
                index = the_menu.desserts.NextIndexDesserts;

                Desserts d11 = new Desserts(input_name.Text, input_price.Text, allergenic_ingredients_input.Text);

                the_menu.desserts[index] = new Desserts();
                the_menu.desserts[index] = d11;
            }
            else if (dish_type_.Text.Equals("Mains"))
            {
                if (main_type_.Text.Equals("Vegan"))
                {
                    index = the_menu.vegans.NextIndexVegan;

                    Vegan_dishes v1 = new Vegan_dishes(input_name.Text,input_price.Text, allergenic_ingredients_input.Text, dish_description_input.Text);

                    the_menu.vegans[index] = new Vegan_dishes();
                    the_menu.vegans[index] = v1;
                }
                else if (main_type_.Text.Equals("Meat"))
                {
                    index = the_menu.meats.NextIndexMeat;

                    Meat_dishes m1 = new Meat_dishes(input_name.Text, input_price.Text, allergenic_ingredients_input.Text, dish_description_input.Text);

                    the_menu.meats[index] = new Meat_dishes();
                    the_menu.meats[index] = m1;
                }

                else if (main_type_.Text.Equals("Dairy"))
                {
                    index = the_menu.dairy.NextIndexDairy;

                    Dairy_dishes da1 = new Dairy_dishes(input_name.Text, input_price.Text, allergenic_ingredients_input.Text, dish_description_input.Text);

                    the_menu.dairy[index] = new Dairy_dishes();
                    the_menu.dairy[index] = da1;
                }
            }
            dish_type_.Text = string.Copy("Select dish type");
            input_name.Text = string.Empty;
            input_price.Text = string.Empty;
            allergenic_ingredients_input.Text = string.Empty;
            dish_description_input.Text = string.Empty;

            if (UsedFile == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    using (Stream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        formatter.Serialize(stream, the_menu);
                    }
                }
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(UsedFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, the_menu);
                }
            }

            add_dish_panel.Visible = false;
            Add_dish.Checked = false;
            dish_type_.Text = string.Copy("Select dish type");
            main_type_.Text= string.Copy("Select main type");
            main_type_.Visible = false;
            description.Visible= false;
            dish_description_input.Visible= false;
        }

        private void log_out_Click(object sender, EventArgs e)
        {
            input_name.Text = string.Empty;
            input_price.Text = string.Empty;
            allergenic_ingredients_input.Text = string.Empty;
            add_dish_panel.Visible = false;
            Add_dish.Checked = false;
            main_type_.Text = string.Copy("Select main type");
            dish_type_.Text = string.Copy("Select dish type");

            Delete_dish.Checked = false;
            delete_select_main_type.Text = string.Copy("Select main type");
            delete_select_dish_type.Text = string.Copy("Select dish type");
            delete_list.Items.Clear();

            Change_dish.Checked = false;
            change_select_main_type.Text = string.Copy("Select main type"); 
            change_select_dish_type.Text = string.Copy("Select dish type");
            change_list.Items.Clear();
            change_select_main_type.Visible = false;
            change_one_dish.Visible = false;
            
            main_type_.Visible = false;
            description.Visible = false;
            dish_description_input.Visible = false;
            create_menu.Visible = false;
            main_screen.Visible = true;
        }

        private void delete_select_dish_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (delete_select_dish_type.Text.Equals("Mains"))
            {
                delete_select_main_type.Visible = true;
            }
            else
            {
                delete_select_main_type.Visible = false;
            }
        }

        private void load_delete_list_Click(object sender, EventArgs e)
        {
            int i;

            delete_list.Items.Clear();

            if (delete_select_dish_type.Text.Equals("Appetizers"))
            {
                for (i = 0; i < the_menu.appetizers.NextIndexAppetizers; i++)
                {
                    delete_list.Items.Add(the_menu.appetizers[i].Dish_name);
                }
            }

            else if (delete_select_dish_type.Text.Equals("Desserts"))
            {
                for (i = 0; i < the_menu.desserts.NextIndexDesserts; i++)
                {
                    delete_list.Items.Add(the_menu.desserts[i].Dish_name);
                }
            }

            else if (delete_select_dish_type.Text.Equals("Mains"))
            {
                if (delete_select_main_type.Text.Equals("Vegan"))
                {
                    for (i = 0; i < the_menu.vegans.NextIndexVegan; i++)
                    {
                        delete_list.Items.Add(the_menu.vegans[i].Dish_name);
                    }
                }

                else if (delete_select_main_type.Text.Equals("Meat"))
                {
                    for (i = 0; i < the_menu.meats.NextIndexMeat; i++)
                    {
                        delete_list.Items.Add(the_menu.meats[i].Dish_name);
                    }

                }

                else if (delete_select_main_type.Text.Equals("Dairy"))
                {
                    for (i = 0; i < the_menu.dairy.NextIndexDairy; i++)
                    {
                        delete_list.Items.Add(the_menu.dairy[i].Dish_name);
                    }
                }
            }
        }

        private void delete_chechked_dishes_Click(object sender, EventArgs e)
        {
            R_U_SURE.Visible = true;
        }

        private void YES_Click(object sender, EventArgs e)
        {
            int i, j;

            if (delete_select_dish_type.Text.Equals("Appetizers"))
            {
                for (i = delete_list.Items.Count - 1; i >= 0; i--)
                {
                    if (delete_list.GetItemChecked(0))
                    {
                        MessageBox.Show("You can't delete chef's recommend");
                    }

                   else if (delete_list.GetItemChecked(i) && i!=0)
                    {
                        for (j = i; j < the_menu.appetizers.Appetizers_dishes_list.Count - 1; j++)
                        {
                            the_menu.appetizers.Appetizers_dishes_list[j] = the_menu.appetizers.Appetizers_dishes_list[j + 1];
                        }
                        the_menu.appetizers.Appetizers_dishes_list.RemoveAt(j);
                        delete_list.Items.Remove(delete_list.Items[i]);
                    }
                }
            }

            else if(delete_select_dish_type.Text.Equals("Desserts"))
            {
                for (i = delete_list.Items.Count - 1; i >= 0; i--)
                {
                    if (delete_list.GetItemChecked(0))
                    {
                        MessageBox.Show("You can't delete chef's recommend");
                    }

                   else if (delete_list.GetItemChecked(i)&& i!=0)
                    {
                        for (j = i; j < the_menu.desserts.Desserts_list.Count - 1; j++)
                        {
                            the_menu.desserts.Desserts_list[j] = the_menu.desserts.Desserts_list[j + 1];
                        }

                        the_menu.desserts.Desserts_list.RemoveAt(j);
                        delete_list.Items.Remove(delete_list.Items[i]);
                    }
                }
            }

            else if (delete_select_dish_type.Text.Equals("Mains"))
            {
                if (delete_select_main_type.Text.Equals("Vegan") )
                {
                    for (i = delete_list.Items.Count - 1; i >= 0; i--)
                    {
                        if (delete_list.GetItemChecked(0))
                        {
                            MessageBox.Show("You can't delete chef's recommend");
                        }

                        else if (delete_list.GetItemChecked(i) && i != 0)
                        {
                            for (j = i; j < the_menu.vegans.Vegan_dishes_list.Count - 1; j++)
                            {
                                the_menu.vegans.Vegan_dishes_list[j] = the_menu.vegans.Vegan_dishes_list[j + 1];
                            }

                            the_menu.vegans.Vegan_dishes_list.RemoveAt(j);
                            delete_list.Items.Remove(delete_list.Items[i]);
                        }
                    }
                }

                else if (delete_select_main_type.Text.Equals("Meat") )
                {
                    for (i = delete_list.Items.Count - 1; i >= 0; i--)
                    {
                        if (delete_list.GetItemChecked(0))
                        {
                            MessageBox.Show("You can't delete chef's recommend");
                        }

                        else if (delete_list.GetItemChecked(i) && i != 0)
                        {
                            for (j = i; j < the_menu.meats.Meat_dishes_list.Count - 1; j++)
                            {
                                the_menu.meats.Meat_dishes_list[j] = the_menu.meats.Meat_dishes_list[j + 1];
                            }
                            the_menu.meats.Meat_dishes_list.RemoveAt(j);
                            delete_list.Items.Remove(delete_list.Items[i]);
                        }
                    }
                }

                else if (delete_select_main_type.Text.Equals("Dairy"))
                {
                    for (i = delete_list.Items.Count - 1; i >= 0; i--)
                    {
                        if (delete_list.GetItemChecked(0))
                        {
                            MessageBox.Show("You can't delete chef's recommend");
                        }

                        else if (delete_list.GetItemChecked(i))
                        {
                            for (j = i; j < the_menu.dairy.Dairy_dishes_list.Count - 1; j++)
                            {
                                the_menu.dairy.Dairy_dishes_list[j] = the_menu.dairy.Dairy_dishes_list[j + 1];
                            }

                            the_menu.dairy.Dairy_dishes_list.RemoveAt(j);
                            delete_list.Items.Remove(delete_list.Items[i]);
                        }
                    }
                }
            }

            if (UsedFile == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    using (Stream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        formatter.Serialize(stream, the_menu);
                    }
                }
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(UsedFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, the_menu);
                }
            }

            delete_select_main_type.Text = string.Copy("Select main type");
            delete_select_main_type.Visible = false;
            add_dish_panel.Visible = false;
            Add_dish.Checked = false;
            delete_select_dish_type.Text = string.Copy("Select dish type");
            delete_list.Items.Clear();
            R_U_SURE.Visible = false;
        }

        private void NO_Click(object sender, EventArgs e)
        {
            R_U_SURE.Visible = false;
        }

        private void change_select_dish_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
          
            change_list.Items.Clear();

            if (change_select_dish_type.Text.Equals("Appetizers"))
            {
                change_select_main_type.Visible = false;
                for (i = 0; i < the_menu.appetizers.NextIndexAppetizers; i++)
                {
                    change_list.Items.Add(the_menu.appetizers[i].Dish_name);
                }
            }

            else if (change_select_dish_type.Text.Equals("Desserts"))
            {
                change_select_main_type.Visible = false;
                for (i = 0; i < the_menu.desserts.NextIndexDesserts; i++)
                {
                    change_list.Items.Add(the_menu.desserts[i].Dish_name);
                }
            }

            else if (change_select_dish_type.Text.Equals("Mains"))
            {
                change_select_main_type.Visible = true;
            }
        }

        private void change_select_main_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            change_list.Items.Clear();

            if (change_select_main_type.Text.Equals("Vegan"))
            {
                for (i = 0; i < the_menu.vegans.NextIndexVegan; i++)
                {
                    change_list.Items.Add(the_menu.vegans[i].Dish_name);
                }
            }

            else if (change_select_main_type.Text.Equals("Meat"))
            {
                for (i = 0; i < the_menu.meats.NextIndexMeat; i++)
                {
                    change_list.Items.Add(the_menu.meats[i].Dish_name);
                }
            }

            else if (change_select_main_type.Text.Equals("Dairy"))
            {
                for (i = 0; i < the_menu.dairy.NextIndexDairy; i++)
                {
                    change_list.Items.Add(the_menu.dairy[i].Dish_name);
                }
            }
        }

        private void Change_buttom_Click(object sender, EventArgs e)
        {
            int i= change_list.CheckedItems.Count, j;

            if (i == 1) 
            {
                for (j = 0; j< change_list.Items.Count; j++)
                {
                    if (change_list.GetItemChecked(0))
                    {
                        change_one_dish.Visible = false;
                        MessageBox.Show("You can't change Chef's recommend!!!");
                    }
                    
                    else if (change_list.GetItemChecked(j))
                    {
                        change_one_dish.Visible = true;
                        if (change_select_dish_type.Text.Equals("Appetizers"))
                        {
                            change_description.Visible = false;
                            change_dish_description.Visible = false;
                            change_dish_name.Text = string.Copy(the_menu.appetizers[j].Dish_name);
                            change_dish_price.Text = string.Copy(the_menu.appetizers[j].Price);
                            change_dish_ai.Text = string.Copy(the_menu.appetizers[j].Allergenic_Ingredients);
                        }
                       
                        else if (change_select_dish_type.Text.Equals("Desserts"))
                        {
                            change_description.Visible = false;
                            change_dish_description.Visible = false;
                            change_dish_name.Text = string.Copy(the_menu.desserts[j].Dish_name);
                            change_dish_price.Text = string.Copy(the_menu.desserts[j].Price);
                            change_dish_ai.Text = string.Copy(the_menu.desserts[j].Allergenic_Ingredients);
                        }
                        
                        else if(change_select_dish_type.Text.Equals("Mains"))
                        {
                            change_description.Visible = true;
                            change_dish_description.Visible = true;

                            if (change_select_main_type.Text.Equals("Vegan"))
                            {
                                change_dish_name.Text = string.Copy(the_menu.vegans[j].Dish_name);
                                change_dish_price.Text = string.Copy(the_menu.vegans[j].Price);
                                change_dish_ai.Text = string.Copy(the_menu.vegans[j].Allergenic_Ingredients);
                                change_dish_description.Text = string.Copy(the_menu.vegans[j].Description);
                            }
                            
                            else if (change_select_main_type.Text.Equals("Meat"))
                            {
                                change_dish_name.Text = string.Copy(the_menu.meats[j].Dish_name);
                                change_dish_price.Text = string.Copy(the_menu.meats[j].Price);
                                change_dish_ai.Text = string.Copy(the_menu.meats[j].Allergenic_Ingredients);
                                change_dish_description.Text = string.Copy(the_menu.meats[j].Description);
                            }
                            
                            else if (change_select_main_type.Text.Equals("Dairy"))
                            {
                                change_dish_name.Text = string.Copy(the_menu.dairy[j].Dish_name);
                                change_dish_price.Text = string.Copy(the_menu.dairy[j].Price);
                                change_dish_ai.Text = string.Copy(the_menu.dairy[j].Allergenic_Ingredients);
                                change_dish_description.Text = string.Copy(the_menu.dairy[j].Description);
                            }
                        }
                    }
                }
            }

            else
            {
                change_one_dish.Visible=false;
                MessageBox.Show("You sould choose just one dish for change,\r\n or you can't change Chef's recommend!!!\r\n");
            }
        }

        private void save_changes_bottum_Click(object sender, EventArgs e)
        {
            int j;

            if (change_select_dish_type.Text.Equals("Appetizers"))
            {
                for (j = 0; j < change_list.Items.Count; j++)
                {
                    if (change_list.GetItemChecked(j))
                    {
                        the_menu.appetizers[j].Dish_name = string.Copy(change_dish_name.Text);
                        the_menu.appetizers[j].Price = string.Copy(change_dish_price.Text);
                        the_menu.appetizers[j].Allergenic_Ingredients = string.Copy(change_dish_ai.Text);
                    }
                }
            }

            else if (change_select_dish_type.Text.Equals("Desserts"))
            {
                for (j = 0; j < change_list.Items.Count; j++)
                {
                    if (change_list.GetItemChecked(j))
                    {
                        the_menu.desserts[j].Dish_name = string.Copy(change_dish_name.Text);
                        the_menu.desserts[j].Price = string.Copy(change_dish_price.Text);
                        the_menu.desserts[j].Allergenic_Ingredients = string.Copy(change_dish_ai.Text);
                    }
                }
            }

            else if (change_select_dish_type.Text.Equals("Mains"))
            {
                if (change_select_main_type.Text.Equals("Vegan"))
                {
                    for (j = 0; j < change_list.Items.Count; j++)
                    {
                        if (change_list.GetItemChecked(j))
                        {
                            the_menu.vegans[j].Dish_name = string.Copy(change_dish_name.Text);
                            the_menu.vegans[j].Price = string.Copy(change_dish_price.Text);
                            the_menu.vegans[j].Allergenic_Ingredients = string.Copy(change_dish_ai.Text);
                            the_menu.vegans[j].Description = string.Copy(change_dish_description.Text);
                        }
                    }
                }

                else if (change_select_main_type.Text.Equals("Meat"))
                {
                    for (j = 0; j < change_list.Items.Count; j++)
                    {
                        if (change_list.GetItemChecked(j))
                        {
                            the_menu.meats[j].Dish_name = string.Copy(change_dish_name.Text);
                            the_menu.meats[j].Price = string.Copy(change_dish_price.Text);
                            the_menu.meats[j].Allergenic_Ingredients = string.Copy(change_dish_ai.Text);
                            the_menu.meats[j].Description = string.Copy(change_dish_description.Text);
                        }
                    }
                }

                else if (change_select_main_type.Text.Equals("Dairy"))
                {
                    for (j = 0; j < change_list.Items.Count; j++)
                    {
                        if (change_list.GetItemChecked(j))
                        {
                            the_menu.dairy[j].Dish_name = string.Copy(change_dish_name.Text);
                            the_menu.dairy[j].Price = string.Copy(change_dish_price.Text);
                            the_menu.dairy[j].Allergenic_Ingredients = string.Copy(change_dish_ai.Text);
                            the_menu.dairy[j].Description = string.Copy(change_dish_description.Text);
                        }
                    }
                }
            }

            if (UsedFile == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    using (Stream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        formatter.Serialize(stream, the_menu);
                    }
                }
            }

            else
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(UsedFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, the_menu);
                }
            }
            change_select_main_type.Text = string.Copy("Select main type");
            change_select_main_type.Visible = false;
            change_one_dish.Visible = false;
            change_list.Items.Clear();
            change_select_dish_type.Text = string.Copy("Select dish type");
        }

        private void recommend_dishs_Click(object sender, EventArgs e)
        {
            recommendations_panel.Visible = true;
            main_screen.Visible = false;
        }

        private void allMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsedFile == null)
            {
                MessageBox.Show("Load menu to continue or create a new menu!!!");
            }
            else
            {
                The_menu_ap_list.Items.Clear();
                The_menu_vegan_list.Items.Clear();
                The_menu_meats_list.Items.Clear();
                The_menu_dairy_list.Items.Clear();
                The_menu_desserts_list.Items.Clear();

                start_panel.Visible = false;
                create_menu.Visible = false;
                recommendations_panel.Visible = false;
                The_total_menu.Visible = false;
                main_screen.Visible = true;
            }
        }

        private void Arencini_Click(object sender, EventArgs e)
        {
            arencini_pic.Visible = true;
            recommend_description.Text = string.Empty;
            sinta_pic.Visible = false;
            hummus_pic.Visible = false;
            tiramisu_pic.Visible = false;  
        }

        private void Tiramisu_Click(object sender, EventArgs e)
        {
            tiramisu_pic.Visible = true;
            recommend_description.Text = string.Empty;
            arencini_pic.Visible = false;
            sinta_pic.Visible = false;
            hummus_pic.Visible = false;
        }

        private void Sinta_Steak_Click(object sender, EventArgs e)
        {
            sinta_pic.Visible = true;
            recommend_description.Text = the_menu.meats[0].Description;
            tiramisu_pic.Visible = false;
            arencini_pic.Visible = false;
            hummus_pic.Visible = false;
        }

        private void Hummus_Click(object sender, EventArgs e)
        {
            hummus_pic.Visible = true;
            recommend_description.Text = the_menu.vegans[0].Description;
            sinta_pic.Visible = false;
            tiramisu_pic.Visible = false;
            arencini_pic.Visible = false;
        }

        private void back_to_main_screen_Click(object sender, EventArgs e)
        {
            recommendations_panel.Visible = false;
            main_screen.Visible = true;
        }


        private void load_menu_to_lists()
        {
            int i;
            The_menu_ap_list.Items.Clear();
            The_menu_vegan_list.Items.Clear();
            The_menu_meats_list.Items.Clear();
            The_menu_dairy_list.Items.Clear();
            The_menu_desserts_list.Items.Clear();
            recommendations_panel.Visible = false;

            for (i = 0; i < the_menu.appetizers.NextIndexAppetizers; i++)
            {
                The_menu_ap_list.Items.Add(the_menu.appetizers[i].Dish_name + " " + the_menu.appetizers[i].Price + " NIS");
            }
            for (i = 0; i < the_menu.vegans.NextIndexVegan; i++)
            {
                The_menu_vegan_list.Items.Add(the_menu.vegans[i].Dish_name + " " + the_menu.vegans[i].Price + " NIS");
            }
            for (i = 0; i < the_menu.meats.NextIndexMeat; i++)
            {
                The_menu_meats_list.Items.Add(the_menu.meats[i].Dish_name + " " + the_menu.meats[i].Price + " NIS");
            }
            for (i = 0; i < the_menu.dairy.NextIndexDairy; i++)
            {
                The_menu_dairy_list.Items.Add(the_menu.dairy[i].Dish_name + " " + the_menu.dairy[i].Price + " NIS");
            }
            for (i = 0; i < the_menu.desserts.NextIndexDesserts; i++)
            {
                The_menu_desserts_list.Items.Add(the_menu.desserts[i].Dish_name + " " + the_menu.desserts[i].Price + " NIS");
            }
        }

        private void appetizersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsedFile == null)
            {
                MessageBox.Show("Load menu to continue or create a new menu!!!");
            }
            else
            {
                load_menu_to_lists();

                The_total_menu.Visible = true;
                main_screen.Visible = false;
                start_panel.Visible = false;
                The_menu_desserts.Visible = false;
                The_menu_desserts_list.Visible = false;
                The_menu_mains.Visible = false;
                panel_total_menu_mains.Visible = false;

                The_menu_ap.Visible = true;
                The_menu_ap_list.Visible = true;
            }
        }

        private void dessertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsedFile == null)
            {
                MessageBox.Show("Load menu to continue or create a new menu!!!");
            }
            else
            {
                load_menu_to_lists();
                The_total_menu.Visible = true;
                start_panel.Visible = false;
                The_menu_mains.Visible = false;
                panel_total_menu_mains.Visible = false;
                The_menu_ap.Visible = false;
                The_menu_ap_list.Visible = false;
                main_screen.Visible = false;

                The_menu_desserts.Visible = true;
                The_menu_desserts_list.Visible = true;
            }
        }

        private void mainsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsedFile == null)
            {
                MessageBox.Show("Load menu to continue or create a new menu!!!");
            }
            else
            {
                load_menu_to_lists();
                The_total_menu.Visible = true;
                main_screen.Visible = false;
                start_panel.Visible = false;
                The_menu_desserts.Visible = false;
                The_menu_desserts_list.Visible = false;
                The_menu_ap.Visible = false;
                The_menu_ap_list.Visible = false;

                The_menu_mains.Visible = true;
                panel_total_menu_mains.Visible = true;
            }
        }

        private void Total_menu_Click(object sender, EventArgs e)
        {
            load_menu_to_lists();

            main_screen.Visible = false;
            The_total_menu.Visible = true;
            panel_total_menu_mains.Visible = true;
            The_menu_ap_list.Visible = true;
            The_menu_desserts_list.Visible = true;
            The_menu_ap.Visible = true;
            The_menu_desserts.Visible = true;
            The_menu_mains.Visible=true;
        }

        private void tabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(UsedFile == null)
            {
                MessageBox.Show("Load menu to continue or create a new menu!!!");
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsedFile == null)
            {
                MessageBox.Show("Load menu to continue or create a new menu!!!");
            }
        }

        private void Show_dishes_Click(object sender, EventArgs e)
        {
            int i;
           
            panel2.Visible = false;
            choosen_dishes.Text = string.Empty;
            The_total_menu.Visible = false;
            bill.Visible = true;
            panel1.Visible = true;
            Show_dishes_panel.Visible = true;

            for (i = 0; i < the_menu.appetizers.NextIndexAppetizers; i++)
            {
                if (The_menu_ap_list.GetItemChecked(i))
                {
                    choosen_dishes.Text = string.Copy(choosen_dishes.Text + "- "+the_menu.appetizers[i].Dish_name + " " + the_menu.appetizers[i].Price + " NIS\nAllergenic ingredients: " + the_menu.appetizers[i].Allergenic_Ingredients + "\n\n");
                }
            }

            for (i = 0; i < the_menu.meats.NextIndexMeat; i++)
            {
                if (The_menu_meats_list.GetItemChecked(i))
                {
                    choosen_dishes.Text = string.Copy(choosen_dishes.Text + "- "+the_menu.meats[i].Dish_name + " " + the_menu.meats[i].Price + " NIS\nAllergenic ingredients: " + the_menu.meats[i].Allergenic_Ingredients + ".\n" + the_menu.meats[i].Description + "\n\n");
                }
            }

            for (i = 0; i < the_menu.vegans.NextIndexVegan; i++)
            {
                if(The_menu_vegan_list.GetItemChecked(i))
                {
                  choosen_dishes.Text = string.Copy(choosen_dishes.Text + "- " + the_menu.vegans[i].Dish_name + " " + the_menu.vegans[i].Price + " NIS\nAllergenic ingredients: " + the_menu.vegans[i].Allergenic_Ingredients + ".\n" + the_menu.vegans[i].Description+"\n\n");
                }
            }

            for (i = 0; i < the_menu.dairy.NextIndexDairy; i++)
            {
                if (The_menu_dairy_list.GetItemChecked(i))
                {
                    choosen_dishes.Text = string.Copy(choosen_dishes.Text + "- " + the_menu.dairy[i].Dish_name + " " + the_menu.dairy[i].Price + " NIS\nAllergenic ingredients: " + the_menu.dairy[i].Allergenic_Ingredients + ".\n" + the_menu.dairy[i].Description + "\n\n");
                }
            }

            for (i = 0; i < the_menu.desserts.NextIndexDesserts; i++)
            {
                if (The_menu_desserts_list.GetItemChecked(i))
                {
                    choosen_dishes.Text = string.Copy(choosen_dishes.Text + "- " + the_menu.desserts[i].Dish_name + " " + the_menu.desserts[i].Price + " NIS\nAllergenic ingredients: " + the_menu.desserts[i].Allergenic_Ingredients+"\n\n");
                }
            }
        }

        private void Back_to_nenu_Click(object sender, EventArgs e)
        {
            Show_dishes_panel.Visible = false;
            The_total_menu.Visible = true;
        }

        private void bill_Click(object sender, EventArgs e)
        {
            int i;

            Score_dishes.Visible = true;
            panel1.Visible = false;
            Back_to_menu.Visible = false;
            bill.Visible = false;
            panel2.Visible = true;
            int total_bill_value = 0;
            Total_bill.Text = string.Empty;

            Total_bill.Text = string.Copy("\n THE BILL IS:\n\n");

            for (i = 0; i < the_menu.appetizers.NextIndexAppetizers; i++)
            {
                if (The_menu_ap_list.GetItemChecked(i))
                {
                    Total_bill.Text = string.Copy(Total_bill.Text + "- " + the_menu.appetizers[i].Dish_name + " " + the_menu.appetizers[i].Price + " NIS \n\n");
                    total_bill_value+= int.Parse(the_menu.appetizers[i].Price);
                }
            }

            for (i = 0; i < the_menu.meats.NextIndexMeat; i++)
            {
                if (The_menu_meats_list.GetItemChecked(i))
                {
                    Total_bill.Text = string.Copy(Total_bill.Text + "- " + the_menu.meats[i].Dish_name + " " + the_menu.meats[i].Price + " NIS \n\n");
                    total_bill_value += int.Parse(the_menu.meats[i].Price);
                }
            }

            for (i = 0; i < the_menu.vegans.NextIndexVegan; i++)
            {
                if (The_menu_vegan_list.GetItemChecked(i))
                {
                    Total_bill.Text = string.Copy(Total_bill.Text + "- " + the_menu.vegans[i].Dish_name + " " + the_menu.vegans[i].Price + " NIS \n\n");
                    total_bill_value += int.Parse(the_menu.vegans[i].Price);
                }
            }

            for (i = 0; i < the_menu.dairy.NextIndexDairy; i++)
            {
                if (The_menu_dairy_list.GetItemChecked(i))
                {
                    Total_bill.Text = string.Copy(Total_bill.Text + "- " + the_menu.dairy[i].Dish_name + " " + the_menu.dairy[i].Price + " NIS \n\n");
                    total_bill_value += int.Parse(the_menu.dairy[i].Price);
                }
            }

            for (i = 0; i < the_menu.desserts.NextIndexDesserts; i++)
            {
                if (The_menu_desserts_list.GetItemChecked(i))
                {
                    Total_bill.Text = string.Copy(Total_bill.Text + "- " + the_menu.desserts[i].Dish_name + " " + the_menu.desserts[i].Price + " NIS \n\n");
                    total_bill_value += int.Parse(the_menu.desserts[i].Price);
                }
            }
            Total_bill.Text = string.Copy(Total_bill.Text +"Price: "+ total_bill_value+ "NIS"); 
        }

        private void Score_dishes_Click(object sender, EventArgs e)
        {
            trackBar1.Visible = true;
            Send_score.Visible = true;
        }

        private void Send_score_Click(object sender, EventArgs e)
        {
            Thank_you thanks= new Thank_you();
            thanks.Show();

            The_menu_ap_list.Items.Clear();
            The_menu_vegan_list.Items.Clear();
            The_menu_meats_list.Items.Clear();
            The_menu_dairy_list.Items.Clear();
            The_menu_desserts_list.Items.Clear();
            
            The_total_menu.Visible = false;
            Show_dishes_panel.Visible = false;
            trackBar1.Visible = false;
            Send_score.Visible = false;
            main_screen.Visible = true;
            Back_to_menu.Visible = true;
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsedFile == null)
            {
                MessageBox.Show("Load menu to continue or create a new menu!!!");
            }
        }

        
    }
}
