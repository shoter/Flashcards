using Common.EntityFramework.SingleChanges;
using Flashcards.Entities;
using Management.Resources;
using Management.Settings;
using Management.Utils;
using Management.ViewModels;
using Ninject;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XmlService xmlService = new XmlService();
        FlashcardUnit unit = SociatisNinject.Current.Get<FlashcardUnit>();
        public int CurrentFlashcardID { get; set; }

        Dictionary<int, List<TranslationViewModel>> Translations { get; set; } = new Dictionary<int, List<TranslationViewModel>>();
        public ObservableCollection<TranslationViewModel> FilteredTranslations { get; set; } = new ObservableCollection<TranslationViewModel>();
        public MainWindow()
        {

            UserSettings.LoadFromXml(xmlService, AppData.UserSettings);
            Images.Init();
            InitializeComponent();
            InitializeLanguages();

            TranslationsGrid.ItemsSource = FilteredTranslations;
        }

        private void InitializeLanguages()
        {
            var languages = unit.LanguageRepository.GetAll();

            foreach (var lang in languages)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = lang.EnglishName;
                item.Tag = lang.ID;
                AvailableLanguages.Items.Add(item);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            UserSettings.SaveToXml(xmlService, AppData.UserSettings);
            base.OnClosed(e);
        }

        private void searchPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            displayFoundFlashcards(unit.FlashcardRepository.SearchForFlashcard(FlashcardSearch.Text));
        }

        private void displayFoundFlashcards(List<Flashcard> flashcards)
        {
            FoundFlashcards.Items.Clear();
            foreach (var flash in flashcards)
            {
                var item = new ListViewItem();
                item.Tag = flash.ID;
                item.Content = flash.Name;
                item.GotFocus += ShowFocusedFlashcard;

                FoundFlashcards.Items.Add(item);
            }
        }

        private void ShowFocusedFlashcard(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null)
            {
                AvailableLanguages.IsEnabled = true;
                TranslationsGrid.IsEnabled = true;
                AddNewTranslationButton.IsEnabled = true;
                SaveEverythingButton.IsEnabled = true;

                int id = (int)item.Tag;
                loadTranslations(id);






            }
        }

        private void loadTranslations(int flashcardID)
        {
            var translations = unit.FlashcardTranslationRepository.GetTranslationsForFlashcard(flashcardID);
            Translations = translations
                .GroupBy(t => t.LanguageID)
                .ToDictionary(x => x.Key, x => x.Select(t => new TranslationViewModel(t)).ToList());

            filterTranslationIfAble();
        }

        private void filterTranslationIfAble()
        {
            FilteredTranslations.Clear();
            var combo = (AvailableLanguages.SelectedItem as ComboBoxItem);
            if (combo != null)
            {
                var langID = (int)combo.Tag;
                if (Translations.ContainsKey(langID))
                    Translations[langID].ForEach(t => FilteredTranslations.Add(t));
            }
        }

        private void AddNewTranslation(object sender, RoutedEventArgs e)
        {
            var combo = (AvailableLanguages.SelectedItem as ComboBoxItem);
            if (combo != null)
            {
                var langID = (int)combo.Tag;
                var vm = new TranslationViewModel()
                {
                    Translation = "Translation",
                    Pronounciation = "Pronounce",
                    Significance = 1.0,
                };

                if (Translations.ContainsKey(langID))
                    Translations[langID].Add(vm);
                else
                    Translations.Add(langID, new List<TranslationViewModel>() { vm });

                FilteredTranslations.Add(vm);
            }
        }

        private void languageChanges(object sender, SelectionChangedEventArgs e)
        {
            filterTranslationIfAble();
        }

        private void saveEverything(object sender, RoutedEventArgs e)
        {
            saveTranslations();
        }

        private void saveTranslations()
        {
            var existingIDs = Translations.Values
                .SelectMany(t => t)
                .Where(t => t.ID.HasValue)
                .Select(t => t.ID);

            var existing = unit.FlashcardTranslationRepository.
                Where(t => existingIDs.Contains(t.ID))
                .ToDictionary(x => x.ID, x => x);

            Dictionary<TranslationViewModel, FlashcardTranslation> newT = new Dictionary<TranslationViewModel, FlashcardTranslation>();


            foreach (var langID in Translations.Keys)
                foreach (var translation in Translations[langID])
                {
                    FlashcardTranslation dbT = null;
                    if (translation.ID.HasValue)
                        dbT = existing[translation.ID.Value];
                    else
                    {
                        dbT = new FlashcardTranslation();
                        newT.Add(translation, dbT);
                    }
                    
                    dbT.Translation = translation.Translation;
                    dbT.Pronounciation = translation.Pronounciation;
                    dbT.Significance = (decimal)translation.Significance;

                    unit.FlashcardTranslationRepository.Add(dbT);
                }

            unit.FlashcardTranslationRepository.SaveChanges();


            foreach (var langID in Translations.Keys)
                foreach (var translation in Translations[langID])
                {
                    if (translation.ID.HasValue == false)
                    {
                        translation.ID = newT[translation].ID;
                    }
                }


        }
    }
}
