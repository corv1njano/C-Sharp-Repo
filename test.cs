using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DEVE_rel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml which is the default frame.
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] typeReplacementsColor = { "White", "Orange", "Yellow", "Red", "Pink", "Magenta", "Purple", "Blue", "Light Blue", "Cyan", "Lime", "Green", "Brown", "Light Gray", "Gray", "Black" };
        private string[] blockTargetsColor = { "Terracotta", "Glazed Terracotta", "Concrete", "Concrete Powder", "Stained Glass", "Stained Glass Pane", "Banner", "Wall Banner", "Candle", "Candle Cake", "Bed", "Wool", "Carpet" };
        private string[] typeReplacementsWood = { "Oak", "Birch", "Spruce", "Dark Oak", "Jungle", "Acacia", "Mangrove", "Cherry", "Pale Oak", "Warped", "Crimson" };
        private string[] blockTargetsWood = { "Planks", "Stairs", "Slab", "Fence", "Fence Gate", "Door", "Trapdoor", "Button", "Pressure Plate", "Sign", "Hanging Sign", "Log/Stem", "Wood/Hyphae" };

        public MainWindow()
        {
            InitializeComponent();
            Icon = new BitmapImage(new Uri("pack://application:,,,/resources/icon.ico"));
        }

        private async void generateFiles_Click(object sender, RoutedEventArgs e)
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
                            await generateMcFunctionAsync(fileContent, color);
                        }
                    }
                    else
                    {
                        foreach (string wood in typeReplacementsWood)
                        {
                            // ...
                        }
                    }

                    checkIfFilesHaveBeenGenerated();
                }
                else
                {
                    MessageBox.Show("One or more Input Fields are empty." +
                        "\n\nPlease input valid Data for the followig Fields and try again: " +
                        "\n- Command" +
                        "\n- Value Replacement" +
                        "\n- Block Target" +
                        "\n- Target Replacement", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Must select a Block Type.\n\nPlease select any Block Type from the Dropdown Menu.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task generateMcFunctionAsync(string fileContent, string replaceType)
        {
            string filePath;

            if (inputLocation.Text == "Same as .exe-File")
            {
                filePath = Path.Combine(getExePath(), $"{replaceType.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction");
            }
            else
            {
                if (Directory.Exists(inputLocation.Text))
                {
                    filePath = Path.Combine(inputLocation.Text, $"{replaceType.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction");
                }
                else
                {
                    MessageBox.Show($"Path not found.\n\nThe given File Output directory does not exist. Cannot create the File '{replaceType.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction'. Please choose a valid Path and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            await File.WriteAllTextAsync(filePath, fileContent);
        }

        private void checkIfFilesHaveBeenGenerated()
        {
            string saveLocation = inputLocation.Text;
            if (saveLocation == "Same as .exe-File")
            {
                saveLocation = getExePath();
            }

            List<string> files = new List<string>();
            if (selectType.SelectedIndex == 0)
            {
                foreach (string color in typeReplacementsColor)
                {
                    string fileName = $"{color.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction";
                    files.Add(fileName);
                }
            }
            else
            {
                // ...
            }

            string failedFiles = "";
            int failedCounter = 0;

            foreach (string file in files)
            {
                string filePath = Path.Combine(saveLocation, file);

                if (!File.Exists(filePath))
                {
                    failedFiles += "- " + file + "\n";
                    failedCounter++;
                }
            }

            if (failedCounter > 0)
            {
                MessageBox.Show($"File(s) could not be generated.\n\n{failedCounter} File(s) could not be placed in the Directory {saveLocation}. Affected Files:\n{failedFiles}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show($"Files successfully generated.\n\n{files.Count} Files have been placed in the Directory {saveLocation}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                foreach (string replacement in typeReplacementsColor)
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

            string exePath = getExePath();

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

        private string getExePath()
        {
            var exeLocation = Assembly.GetExecutingAssembly().Location;
            var exePath = Path.GetDirectoryName(exeLocation);

            if (exePath == null)
            {
                MessageBox.Show("Path not found.\n\nExe-Path could not be determined.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                exePath = String.Empty;
            }

            return exePath;
        }

        private void openGithub(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/corv1njano/Display-Entities-Scripts") { UseShellExecute = true });
        }

        private void openTutorial(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/corv1njano/Display-Entities-Scripts/blob/main/docs/Help%20and%20Tutorial.md") { UseShellExecute = true });
        }

        private void checkForUpdate(object sender, RoutedEventArgs e)
        {
            const string UpdateUrl = "https://raw.githubusercontent.com/corv1njano/Display-Entities-Scripts/refs/heads/main/.appversion";
            const int CurrentVersion = 150;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string fileContent = client.GetStringAsync(UpdateUrl).Result;
                    
                    if (fileContent != null)
                    {
                        if (int.Parse(fileContent) > CurrentVersion)
                        {
                            MessageBoxResult result = MessageBox.Show("A new Update is available.\n\nWould you like to visit the Download Page?", "Check for Updates...", MessageBoxButton.YesNo, MessageBoxImage.Information);
                            if (result == MessageBoxResult.Yes)
                            {
                                Process.Start(new ProcessStartInfo("https://github.com/corv1njano/Display-Entities-Scripts/releases") { UseShellExecute = true });
                            }
                        }
                        else if (int.Parse(fileContent) <= CurrentVersion)
                        {
                            MessageBox.Show("You are up to Date.\n\nNo new Updates available at the Moment.", "Check for Updates...", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("An Error occured.\n\nWhile processing the received Data from the Server an unexpected Error occurred. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"An Error occured.\n\nWhile connecting to the Server an unexpected Error occurred. Maybe check your Internet Connection and try again.\n\nError Message: {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
