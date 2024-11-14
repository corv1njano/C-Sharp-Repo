using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DEVE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] blockTargetsColor = { "Terracotta", "Glazed Terracotta", "Concrete", "Concrete Powder", "Stained Glass", "Stained Glass Pane", "Banner", "Wall Banner", "Candle", "Candle Cake", "Bed", "Wool", "Carpet" };
        private string[] typeReplacementsColor = { "White", "Orange", "Yellow", "Red", "Pink", "Magenta", "Purple", "Blue", "Light Blue", "Cyan", "Lime", "Green", "Brown", "Light Gray", "Gray", "Black" };
        private string[] typeReplacementsWood = { "Oak", "Birch", "Spruce", "Dark Oak", "Jungle", "Acacia", "Mangrove", "Cherry", "Warped", "Crimson" };
        private string[] blockTargetsWood = { "Planks", "Stairs", "Slab", "Fence", "Fence Gate", "Door", "Trapdoor", "Button", "Pressure Plate", "Sign", "Hanging Sign", "Log", "Wood" };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void generateFiles_Click(object sender, RoutedEventArgs e)
        {
            if (selectType.SelectedValue != null)
            {
                if (!string.IsNullOrWhiteSpace(inputCommand.Text) && !(typeReplacement.SelectedIndex == -1) && !(blockTarget.SelectedIndex == -1) && !(blockReplacement.SelectedIndex == -1))
                {
                    inputCommand.Text = inputCommand.Text.Replace("/summon", "summon");

                    string fileContent;
                    if (selectType.SelectedIndex == 0)
                    {
                        foreach (string color in typeReplacementsColor)
                        {    
                            fileContent = inputCommand.Text.Replace($"{typeReplacement.Text.ToLower().Replace(" ", "_")}_{blockTarget.Text.ToLower().Replace(" ", "_")}", $"{color.ToLower().Replace(" ", "_")}_{blockReplacement.Text.ToLower().Replace(" ", "_")}");

                            if (inputLocation.Text == "Same as .exe-File")
                            {
                                File.WriteAllText($"{color.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction", fileContent);
                            }
                            else
                            {
                                File.WriteAllText($@"{inputLocation.Text}\{color.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction", fileContent);
                            }
                            
                        }
                    }
                    else
                    {
                        foreach (string wood in typeReplacementsWood)
                        {
                            //fileContent = inputCommand.Text.Replace($"{typeReplacement.Text.ToLower().Replace(" ", "_")}_{blockTarget.Text.ToLower().Replace(" ", "_")}", $"{wood.ToLower().Replace(" ", "_")}_{blockReplacement.Text.ToLower().Replace(" ", "_")}");

                            if (typeReplacement.SelectedIndex == 8 || typeReplacement.SelectedIndex == 9)
                            {
                                if (wood == "Warped" || wood == "Crimson")
                                {
                                    fileContent = inputCommand.Text.Replace($"{typeReplacement.Text.ToLower().Replace(" ", "_")}_{blockTarget.Text.ToLower().Replace(" ", "_")}", $"{wood.ToLower().Replace(" ", "_")}_{blockReplacement.Text.ToLower().Replace(" ", "_")}");
                                }
                                else
                                {
                                    fileContent = inputCommand.Text.Replace($"{typeReplacement.Text.ToLower().Replace(" ", "_")}_{blockTarget.Text.ToLower().Replace(" ", "_")}", $"{wood.ToLower().Replace(" ", "_")}_{blockReplacement.Text.ToLower().Replace(" ", "_")}");
                                }
                            }
                        }
                    }
                    
                }
                else
                {
                    string err_fillOut = "One or more input fields are empty." +
                        "\n\nPlease input valid data for the followig fields and try again: " +
                        "\n- Command" +
                        "\n- Value Replacement" +
                        "\n- Block Target" +
                        $"\n- Target Replacement";
                    MessageBox.Show(err_fillOut, "Error");
                }
            }
            else
            {
                string err_fillOut = "Must select a Block Type.\n\nPlease select any Block Type from the dropdown menu.";
                MessageBox.Show(err_fillOut, "Error");
            }
        }

        private void selectType_Change(object sender, SelectionChangedEventArgs e)
        {
            typeReplacement.IsEnabled = true;
            blockTarget.IsEnabled = true;
            blockReplacement.IsEnabled = true;

            if (selectType.SelectedIndex == 0)
            {
                typeReplacement.Items.Clear();
                foreach (var replacement in typeReplacementsColor)
                {
                    typeReplacement.Items.Add(new ComboBoxItem { Content = replacement });
                }

                blockTarget.Items.Clear();
                blockReplacement.Items.Clear();
                foreach (string target in blockTargetsColor)
                {
                    blockTarget.Items.Add(new ComboBoxItem { Content = target });
                    blockReplacement.Items.Add(new ComboBoxItem { Content = target });
                }
            }
            else
            {
                typeReplacement.Items.Clear();
                foreach (string replacement in typeReplacementsWood)
                {
                    typeReplacement.Items.Add(new ComboBoxItem { Content = replacement });
                }

                blockTarget.Items.Clear();
                blockReplacement.Items.Clear();
                foreach (string target in blockTargetsWood)
                {
                    blockTarget.Items.Add(new ComboBoxItem { Content = target });
                    blockReplacement.Items.Add(new ComboBoxItem { Content = target });
                }
            }
        }

        private void inputLocation_Click(object sender, MouseButtonEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Title = "Select a Folder for your .mcfunction-Files...";
            dialog.IsFolderPicker = true;

            string exeLocation = Assembly.GetExecutingAssembly().Location;
            string exePath = Path.GetDirectoryName(exeLocation);

            if (inputLocation.Text == "Same as .exe-File")
            {
                dialog.InitialDirectory = exePath;
            }
            else
            {
                dialog.InitialDirectory = inputLocation.Text;
            }

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                inputLocation.Text = dialog.FileName;
            }
        }

        private void openGithub(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/corv1njano/Display-Entities-Scripts") { UseShellExecute = true });
        }

        private void openTutorial(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/corv1njano/Display-Entities-Scripts/blob/main/docs/Help%20and%20Tutorial.md") { UseShellExecute = true });
        }

        private void resetForm(object sender, RoutedEventArgs e)
        {
            inputCommand.Text = string.Empty;
            selectType.SelectedIndex = -1;
            typeReplacement.Items.Clear();
            blockTarget.Items.Clear();
            blockReplacement.Items.Clear();
            typeReplacement.IsEnabled = false;
            blockTarget.IsEnabled = false;
            blockReplacement.IsEnabled = false;
            inputFilename.Text = string.Empty;
            inputLocation.Text = "Same as .exe-File";
        }
    }
}
