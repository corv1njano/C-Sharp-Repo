using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Version number as an integer, must be in a 3 digit format
        private const int CurrentVersion = 103;

        private string[] typeReplacementsColor = { "White", "Orange", "Yellow", "Red", "Pink", "Magenta", "Purple", "Blue", "Light Blue", "Cyan", "Lime", "Green", "Brown", "Light Gray", "Gray", "Black" };
        private string[] blockTargetsColor = { "Terracotta", "Glazed Terracotta", "Concrete", "Concrete Powder", "Stained Glass", "Stained Glass Pane", "Banner", "Wall Banner", "Candle", "Candle Cake", "Bed", "Wool", "Carpet" };
        private string[] typeReplacementsWood = { "Oak", "Birch", "Spruce", "Dark Oak", "Jungle", "Acacia", "Crimson", "Warped", "Mangrove", "Cherry", "Pale Oak" };
        private string[] blockTargetsWood = { "Planks", "Log/Stem", "Wood/Hyphae", "Stairs", "Slab", "Fence", "Fence Gate", "Door", "Trapdoor", "Button", "Pressure Plate", "Sign", "Hanging Sign"};

        public MainWindow()
        {
            InitializeComponent();
            Icon = new BitmapImage(new Uri("pack://application:,,,/icon.ico"));
            infoDisplay.Text = $"Version {getStringVersion(CurrentVersion)} by corv1njano";
        }
        private string getStringVersion(int version)
        {
            string versionAsString = version.ToString();
            string formattedVersion = "";
            for (int i = 0; i < versionAsString.Length; i++)
            {
                formattedVersion += (i == 2) ? versionAsString[i] : versionAsString[i] + ".";
            }

            return formattedVersion;
        }

        private async void generateFiles_Click(object sender, RoutedEventArgs e)
        {
            if (selectType.SelectedValue == null)
            {
                showDialog("Error", "Must select a block type.", "Please select any block type from the dropdown menu.", DialogModal.DialogType.Ok, DialogModal.DialogSound.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(inputCommand.Text) || (typeReplacement.SelectedIndex == -1) || (blockTarget.SelectedIndex == -1) || (blockReplacement.SelectedIndex == -1))
            {
                showDialog("Error", "One or more input fields are empty.", "Please enter valid data for the following fields and try again:\n- Command\n- Value replacement\n- Block target\n- Target replacement", DialogModal.DialogType.Ok, DialogModal.DialogSound.Warning);
                return;
            }

            if (inputFilename.Text.Length > 80)
            {
                showDialog("Error", "File name too long.", $"Please keep the filename under 80 characters. Your current filename is {inputFilename.Text.Length} characters long.", DialogModal.DialogType.Ok, DialogModal.DialogSound.Warning);
                return;
            }

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
                if (blockTarget.SelectedIndex == 1 || blockTarget.SelectedIndex == 2 || blockReplacement.SelectedIndex == 1 || blockReplacement.SelectedIndex == 2)
                {
                    inputCommand.Text = inputCommand.Text.Replace($"{blockTarget.Text}", $"{blockTarget.Text.Replace("Log/Stem", "log")}").Replace($"{blockTarget.Text}", $"{blockTarget.Text.Replace("Wood/Hyphae", "wood")}");
                    inputCommand.Text = inputCommand.Text.Replace($"{blockReplacement.Text}", $"{blockReplacement.Text.Replace("Log/Stem", "log")}").Replace($"{blockReplacement.Text}", $"{blockReplacement.Text.Replace("Wood/Hyphae", "wood")}");

                    if (typeReplacement.SelectedIndex == 6 || typeReplacement.SelectedIndex == 7)
                    {
                        foreach (string wood in typeReplacementsWood)
                        {
                            if (wood == "Crimson" || wood == "Warped")
                            {

                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        foreach (string wood in typeReplacementsWood)
                        {
                            if (wood == "Crimson" || wood == "Warped")
                            {

                            }
                            else
                            {

                            }
                        }
                    }

                    //foreach (string wood in typeReplacementsWood)
                    //{
                    //    if (typeReplacement.SelectedIndex == 6 || typeReplacement.SelectedIndex == 7)
                    //    {
                    //        if (wood == "Crimson" || wood == "Warped")
                    //        {
                    //            fileContent = inputCommand.Text.Replace($"{typeReplacement.Text.ToLower().Replace(" ", "_")}_{blockTarget.Text.ToLower().Replace(" ", "_")}", $"{wood.ToLower().Replace(" ", "_")}_{blockReplacement.Text.ToLower().Replace(" ", "_")}");
                    //            await generateMcFunctionAsync(fileContent, wood);
                    //        }
                    //        else
                    //        {
                    //            fileContent = inputCommand.Text.Replace($"{typeReplacement.Text.ToLower().Replace(" ", "_")}_{blockTarget.Text.ToLower().Replace(" ", "_")}", $"{wood.ToLower().Replace(" ", "_")}_{blockReplacement.Text.ToLower().Replace(" ", "_")}");
                    //            fileContent = fileContent.Replace("stem", "log").Replace("hyphae", "wood");

                    //            DialogModal dialog = new DialogModal($"{typeReplacement.Text} , {fileContent}") { Owner = this };
                    //            dialog.ShowDialog();

                    //            await generateMcFunctionAsync(fileContent, wood);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (wood == "Crimson" || wood == "Warped")
                    //        {
                    //            fileContent = inputCommand.Text.Replace($"{typeReplacement.Text.ToLower().Replace(" ", "_")}_{blockTarget.Text.ToLower().Replace(" ", "_")}", $"{wood.ToLower().Replace(" ", "_")}_{blockReplacement.Text.ToLower().Replace(" ", "_")}");
                    //            fileContent = fileContent.Replace("log", "stem").Replace("wood", "hyphae");
                    //            await generateMcFunctionAsync(fileContent, wood);
                    //        }
                    //        else
                    //        {
                    //            fileContent = inputCommand.Text.Replace($"{typeReplacement.Text.ToLower().Replace(" ", "_")}_{blockTarget.Text.ToLower().Replace(" ", "_")}", $"{wood.ToLower().Replace(" ", "_")}_{blockReplacement.Text.ToLower().Replace(" ", "_")}");
                    //            await generateMcFunctionAsync(fileContent, wood);
                    //        }
                    //    }
                    //}
                }
                else
                {
                    foreach (string wood in typeReplacementsWood)
                    {
                        fileContent = inputCommand.Text.Replace($"{typeReplacement.Text.ToLower().Replace(" ", "_")}_{blockTarget.Text.ToLower().Replace(" ", "_")}", $"{wood.ToLower().Replace(" ", "_")}_{blockReplacement.Text.ToLower().Replace(" ", "_")}");
                        await generateMcFunctionAsync(fileContent, wood);
                    }
                }
            }

            checkIfFilesHaveBeenGenerated();
        }

        private async Task generateMcFunctionAsync(string fileContent, string replaceType)
        {
            string filePath;

            if (inputLocation.Text == "Same as .exe-file")
            {
                filePath = Path.Combine(getExePath(), $"{replaceType.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction");
            }
            else
            {
                if (!Directory.Exists(inputLocation.Text))
                {
                    showDialog("Error", "Path not found.", $"The given file output directory does not exist. Cannot create the file '{replaceType.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction'. Please choose a valid path and try again.", DialogModal.DialogType.Ok, DialogModal.DialogSound.Warning);
                    return;
                }

                filePath = Path.Combine(inputLocation.Text, $"{replaceType.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction");
            }

            await File.WriteAllTextAsync(filePath, fileContent);
        }

        private void checkIfFilesHaveBeenGenerated()
        {
            string saveLocation = (inputLocation.Text == "Same as .exe-file") ? getExePath() : inputLocation.Text;

            List<string> files = new List<string>();
            files.Clear();

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
                foreach (string wood in typeReplacementsWood)
                {
                    string fileName = $"{wood.ToLower().Replace(" ", "_")}_{inputFilename.Text}.mcfunction";
                    files.Add(fileName);
                }
            }

            string failedFiles = "";
            int failedCounter = 0;

            foreach (string file in files)
            {
                string filePath = Path.Combine(saveLocation, file);

                if (!File.Exists(filePath))
                {
                    failedFiles += "\n- " + file;
                    failedCounter++;
                }
            }

            if (failedCounter > 0)
            {
                string singularPlural = (failedCounter == 1) ? "file" : "files";
                showDialog("Error", $"{char.ToUpper(singularPlural[0]) + singularPlural.Substring(1)} could not be generated.", $"{failedCounter} {singularPlural} could not be placed in the directory {saveLocation}. Affected {singularPlural}:{failedFiles}", DialogModal.DialogType.Ok, DialogModal.DialogSound.Warning);
                return;
            }

            showDialog("Success", "Files successfully generated.", $"{files.Count} files have been placed in the directory {saveLocation}.", DialogModal.DialogType.Ok, DialogModal.DialogSound.Info);
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
            dialog.Title = "Select a folder for your .mcfunction-files...";
            dialog.IsFolderPicker = true;

            string exePath = getExePath();

            dialog.InitialDirectory = (inputLocation.Text == "Same as .exe-file") ? exePath : inputLocation.Text;

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
                showDialog("Error", "Path not found.", "Path of the exe-file could not be determined.", DialogModal.DialogType.Ok, DialogModal.DialogSound.Warning);
                exePath = String.Empty;
            }

            return exePath;
        }

        private void openGithub(object sender, RoutedEventArgs e)
        {
            runUrl("https://github.com/corv1njano/Display-Entities-Scripts");
        }

        private void openTutorial(object sender, RoutedEventArgs e)
        {
            runUrl("https://github.com/corv1njano/Display-Entities-Scripts/blob/main/docs/Help%20and%20Tutorial.md");
        }

        private void checkForUpdate(object sender, RoutedEventArgs e)
        {
            const string UpdateUrl = "https://raw.githubusercontent.com/corv1njano/Display-Entities-Scripts/refs/heads/main/.appversion";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string fileContent = client.GetStringAsync(UpdateUrl).Result;
                    
                    if (fileContent == null)
                    {
                        showDialog("Error", "An Error occured.", "While processing the received data from the server an unexpected error occurred. Please try again.", DialogModal.DialogType.Ok, DialogModal.DialogSound.Warning);
                        return;
                    }

                    int serverVersion;
                    try
                    {
                        serverVersion = int.Parse(fileContent);
                    }
                    catch (FormatException)
                    {
                        bool invalidFormatDialogResult = showDialog("Error", "Invalid file format.", "The received data is in an invalid format. Would you like to report this issue?", DialogModal.DialogType.YesNo, DialogModal.DialogSound.Warning);
                        if (invalidFormatDialogResult == true)
                        {
                            runUrl("https://github.com/corv1njano/Display-Entities-Scripts/issues");
                        }
                        return;
                    }

                    if (serverVersion < CurrentVersion)
                    {
                        showDialog("Check for updates...", "You are up to date", "No new updates available at the moment.", DialogModal.DialogType.Ok, DialogModal.DialogSound.Info);
                    }

                    bool updateDialogResult = showDialog("Check for updates...", "A new version is available.", $"Client version: {getStringVersion(CurrentVersion)}\nServer version: {getStringVersion(serverVersion)}\n\nWould you like to visit the download page?", DialogModal.DialogType.YesNo, DialogModal.DialogSound.Info);
                    if (updateDialogResult == true)
                    {
                        runUrl("https://github.com/corv1njano/Display-Entities-Scripts/releases");
                    }
                }
                catch (Exception exception)
                {
                    showDialog("Error", "An Error occured.", $"While connecting to the Server an unexpected Error occurred. Please and try again.\n\nError Message: {exception.Message}", DialogModal.DialogType.Ok, DialogModal.DialogSound.Warning);
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
            inputLocation.Text = "Same as .exe-file";
        }

        private void runUrl(string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private bool showDialog(string title, string messageHeader, string message, DialogModal.DialogType dialogType, DialogModal.DialogSound dialogSound)
        {
            DialogModal dialog = new DialogModal(title, messageHeader, message, dialogType, dialogSound) { Owner = this };
            bool? dialogResult = dialog.ShowDialog();

            if (dialogType == DialogModal.DialogType.YesNo)
            {
                return (dialogResult == true) ? true : false;
            }

            return true;
        } 
    }
}
