﻿using Arkivverket.Arkade.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Arkivverket.Arkade.UI.Views
{
    /// <summary>
    /// Interaction logic for CreatePackage.xaml
    /// </summary>
    public partial class CreatePackage : UserControl
    {
        public CreatePackage()
        {
            InitializeComponent();
        }

        private void DatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            DatePicker datepicker = (DatePicker)sender;
            Popup popup = (Popup)datepicker.Template.FindName("PART_Popup", datepicker);
            System.Windows.Controls.Calendar cal = (System.Windows.Controls.Calendar)popup.Child;
            cal.DisplayMode = System.Windows.Controls.CalendarMode.Decade;
        }

        private void MetaDataSystemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetLabelText();
        }

        private void MetadataStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetLabelText();
        }

        private void MetadataEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetLabelText();
        }

        private void SetLabelText()
        {
            if(string.IsNullOrEmpty(((CreatePackageViewModel)(this.DataContext)).MetaDataNoarkSection.PackageLabel))
            {
                string label = "";
                var metadataSystemName = MetaDataSystemName.Text;
                if (!string.IsNullOrEmpty(metadataSystemName))
                    label = $"{metadataSystemName}";
                if (!string.IsNullOrEmpty(MetadataStartDate.Text) && !string.IsNullOrEmpty(MetadataEndDate.Text))
                    label += $" ({MetadataStartDate.SelectedDate?.Year} - {MetadataEndDate.SelectedDate?.Year})";

                ((CreatePackageViewModel)(this.DataContext)).MetaDataNoarkSection.PackageLabel = label;
            }
        }
    }
}
