﻿using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Resources;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Scripts;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sprites;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Catrobat.IDEWindowsPhone.ViewModel.Service;
using Catrobat.IDEWindowsPhone.ViewModel.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using IDEWindowsPhone;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.ViewModel
{
    public class ViewModelLocator
    {
        private static CatrobatContextBase _context;

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>(true);
            SimpleIoc.Default.Register<EditorViewModel>(true);
            SimpleIoc.Default.Register<AddNewProjectViewModel>(true);
            SimpleIoc.Default.Register<UploadProjectViewModel>(true);
            SimpleIoc.Default.Register<UploadProjectLoginViewModel>(true);
            SimpleIoc.Default.Register<SoundRecorderViewModel>(true);
            SimpleIoc.Default.Register<SettingsViewModel>(true);
            SimpleIoc.Default.Register<AddNewCostumeViewModel>(true);
            SimpleIoc.Default.Register<ChangeCostumeViewModel>(true);
            SimpleIoc.Default.Register<AddNewSoundViewModel>(true);
            SimpleIoc.Default.Register<ChangeSoundViewModel>(true);
            SimpleIoc.Default.Register<AddNewSpriteViewModel>(true);
            SimpleIoc.Default.Register<ChangeSpriteViewModel>(true);
            SimpleIoc.Default.Register<ProjectSettingsViewModel>(true);
            SimpleIoc.Default.Register<ProjectImportViewModel>(true);
            SimpleIoc.Default.Register<OnlineProjectViewModel>(true);
            SimpleIoc.Default.Register<NewBroadcastMessageViewModel>(true);
            SimpleIoc.Default.Register<AddNewScriptBrickViewModel>(true);
            SimpleIoc.Default.Register<ProjectNotValidViewModel>(true);
            SimpleIoc.Default.Register<FormulaEditorViewModel>(true);
            SimpleIoc.Default.Register<PlayerLauncherViewModel>(true);
        }

        private static void InitializeFirstTimeUse(CatrobatContextBase context)
        {
            Project currentProject = null;
            var localSettings = CatrobatContext.RestoreLocalSettingsStatic();

            if (localSettings == null)
            {
                if (Debugger.IsAttached)
                {
                    var loader = new SampleProjectLoader();
                    loader.LoadSampleProjects();
                }

                currentProject = CatrobatContext.RestoreDefaultProjectStatic(CatrobatContextBase.DefaultProjectName);
                context.LocalSettings = new LocalSettings { CurrentProjectName = currentProject.ProjectHeader.ProgramName };
            }
            else
            {
                context.LocalSettings = localSettings;
                currentProject = CatrobatContext.CreateNewProjectByNameStatic(context.LocalSettings.CurrentProjectName);
            }

            var message = new GenericMessage<Project>(currentProject);
            Messenger.Default.Send<GenericMessage<Project>>(message, ViewModelMessagingToken.CurrentProjectChangedListener);
        }

        public static void LoadContext()
        {

            if (ViewModelBase.IsInDesignModeStatic)
            {
                _context = new CatrobatContextDesign();
            }
            else
            {
                _context = new CatrobatContext();
                InitializeFirstTimeUse(_context);
            }

            if (_context.LocalSettings.CurrentLanguageString == null)
                _context.LocalSettings.CurrentLanguageString = LanguageHelper.GetCurrentCultureLanguageCode();

            var themeChooser = (ThemeChooser)Application.Current.Resources["ThemeChooser"];
            if (_context.LocalSettings.CurrentThemeIndex != -1)
                themeChooser.SelectedThemeIndex = _context.LocalSettings.CurrentThemeIndex;

            if (_context.LocalSettings.CurrentLanguageString != null)
                ServiceLocator.Current.GetInstance<SettingsViewModel>().CurrentCulture = 
                    new CultureInfo(_context.LocalSettings.CurrentLanguageString);

            var message1 = new GenericMessage<ThemeChooser>(themeChooser);
            Messenger.Default.Send<GenericMessage<ThemeChooser>>(message1, ViewModelMessagingToken.ThemeChooserListener);

            var message2 = new GenericMessage<CatrobatContextBase>(_context);
            Messenger.Default.Send<GenericMessage<CatrobatContextBase>>(message2, ViewModelMessagingToken.ContextListener);
        }

        public static void SaveContext(string currentProjectName)
        {
            var themeChooser = (ThemeChooser)Application.Current.Resources["ThemeChooser"];
            var settingsViewModel = ServiceLocator.Current.GetInstance<SettingsViewModel>();

            if (themeChooser.SelectedTheme != null)
            {
                _context.LocalSettings.CurrentThemeIndex = themeChooser.SelectedThemeIndex;
            }

            if (settingsViewModel.CurrentCulture != null)
            {
                _context.LocalSettings.CurrentLanguageString = settingsViewModel.CurrentCulture.Name;
            }

            _context.LocalSettings.CurrentProjectName = currentProjectName;
            CatrobatContext.StoreLocalSettingsStatic(_context.LocalSettings);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewProjectViewModel AddNewProjectViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewProjectViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditorViewModel EditorViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditorViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectSettingsViewModel ProjectSettingsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectSettingsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectViewModel UploadProjectViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UploadProjectViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectLoginViewModel UploadProjectLoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UploadProjectLoginViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SoundRecorderViewModel SoundRecorderViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewCostumeViewModel AddNewCostumeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewCostumeViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeCostumeViewModel ChangeCostumeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChangeCostumeViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeSoundViewModel ChangeSoundViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChangeSoundViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewSoundViewModel AddNewSoundViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewSoundViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeSpriteViewModel ChangeSpriteViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChangeSpriteViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewSpriteViewModel AddNewSpriteViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewSpriteViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectImportViewModel ProjectImportViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectImportViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public OnlineProjectViewModel OnlineProjectViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OnlineProjectViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public NewBroadcastMessageViewModel NewBroadcastMessageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewBroadcastMessageViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewScriptBrickViewModel AddNewScriptBrickViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewScriptBrickViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectNotValidViewModel ProjectNotValidViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectNotValidViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public FormulaEditorViewModel FormulaEditorViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FormulaEditorViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public PlayerLauncherViewModel PlayerLauncherViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayerLauncherViewModel>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}