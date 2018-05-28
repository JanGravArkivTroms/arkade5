﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Arkivverket.Arkade.ExternalModels.Ead;
using Prism.Commands;
using Prism.Mvvm;

namespace Arkivverket.Arkade.UI.Models
{
    public class GuiMetaDataModel : BindableBase
    {
        private Visibility _visibilityItem = Visibility.Visible;
        private Visibility _visibilityAddItem = Visibility.Hidden;
        private Visibility _deleteButtonVisibility = Visibility.Visible;

        public bool IsDeleted = false;

        private string _iconAdd = "PlusCircleOutline";
        private string _iconDelete = "Delete";
        private string _iconNameList = "MenuDown";

        public ICommand CommandDeleteItem { get; set; }
        public ICommand CommandAddItem { get; set; }
        public ICommand CommandNullOutEntry { get; set; }


        public string IconAdd
        {
            get { return _iconAdd; }
            set { SetProperty(ref _iconAdd, value); }
        }

        public string IconDelete
        {
            get { return _iconDelete; }
            set { SetProperty(ref _iconDelete, value); }
        }

        public string IconNameList
        {
            get { return _iconNameList; }
            set { SetProperty(ref _iconNameList, value); }
        }

        public Visibility VisibilityItem
        {
            get { return _visibilityItem; }
            set { SetProperty(ref _visibilityItem, value); }
        }

        public Visibility VisibilityAddItem
        {
            get { return _visibilityAddItem; }
            set { SetProperty(ref _visibilityAddItem, value); }
        }


        public Visibility DeleteButtonVisibility
        {
            get { return _deleteButtonVisibility; }
            set { SetProperty(ref _deleteButtonVisibility, value); }
        }


        private string _archiveDescription;
        private string _agreementNumber;

        private string _entity;
        private string _contactPerson;
        private string _telephone;
        private string _email;

        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }


        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string Telephone
        {
            get { return _telephone; }
            set { SetProperty(ref _telephone, value); }
        }

        public string ContactPerson
        {
            get { return _contactPerson; }
            set { SetProperty(ref _contactPerson, value); }
        }

        public string Entity
        {
            get { return _entity; }
            set { SetProperty(ref _entity, value); }
        }

        private string _systemName;
        private string _systemVersion;
        private string _systemType;
        private string _systemTypeVersion;

        public string SystemName
        {
            get { return _systemName; }
            set { SetProperty(ref _systemName, value); }
        }

        public string SystemVersion
        {
            get { return _systemVersion; }
            set { SetProperty(ref _systemVersion, value); }
        }

        public string SystemType
        {
            get { return _systemType; }
            set { SetProperty(ref _systemType, value); }
        }

        public string SystemTypeVersion
        {
            get { return _systemTypeVersion; }
            set { SetProperty(ref _systemTypeVersion, value); }
        }


        public string ArchiveDescription
        {
            get { return _archiveDescription; }
            set { SetProperty(ref _archiveDescription, value); }
        }

        public string AgreementNumber
        {
            get { return _agreementNumber; }
            set { SetProperty(ref _agreementNumber, value); }
        }


        private string _history;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private DateTime? _extractionDate;
        private string _incommingSeparator;
        private string _outgoingSeparator;
        private string _packageLabel;


        public string History
        {
            get { return _history; }
            set { SetProperty(ref _history, value); }
        }


        public DateTime? StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }


        public DateTime? EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        public DateTime? ExtractionDate
        {
            get { return _extractionDate; }
            set { SetProperty(ref _extractionDate, value); }
        }

        public string IncommingSeparator
        {
            get { return _incommingSeparator; }
            set { SetProperty(ref _incommingSeparator, value); }
        }

        public string OutgoingSeparator
        {
            get { return _outgoingSeparator; }
            set { SetProperty(ref _outgoingSeparator, value); }
        }

        public string PackageLabel
        {
            get { return _packageLabel; }
            set { SetProperty(ref _packageLabel, value); }
        }


        public GuiMetaDataModel(string archiveDescription, string agreementNumber)
        {
            ArchiveDescription = archiveDescription;
            AgreementNumber = agreementNumber;
            CommandDeleteItem = new DelegateCommand(ExecuteDeleteItem);
            CommandAddItem = new DelegateCommand(ExecuteAddItem);
            CommandNullOutEntry = new DelegateCommand(NullOutRecord);
        }


        public GuiMetaDataModel(string entity, string contactPerson, string telephone, string email)
        {
            Entity = entity;
            ContactPerson = contactPerson;
            Telephone = telephone;
            Email = email;

            CommandDeleteItem = new DelegateCommand(ExecuteDeleteItem);
            CommandAddItem = new DelegateCommand(ExecuteAddItem);
            CommandNullOutEntry = new DelegateCommand(NullOutRecord);
        }

        public GuiMetaDataModel(string systemName, string systemVersion, string systemType, string systemTypeVersion, GuiObjectType guiObjectType)
        {

            if (guiObjectType == GuiObjectType.system)
            {
                SystemName = systemName;
                SystemVersion = systemVersion;
                SystemVersion = systemType;
                SystemTypeVersion = systemTypeVersion;
            } 

            CommandDeleteItem = new DelegateCommand(ExecuteDeleteItem);
            CommandAddItem = new DelegateCommand(ExecuteAddItem);
            CommandNullOutEntry = new DelegateCommand(NullOutRecord);
        }


        public GuiMetaDataModel(DateTime startDate, DateTime endDate, string incommingSeparator, string outgoingSeparator)
        {
            StartDate = startDate;
            EndDate = endDate;
            IncommingSeparator = incommingSeparator;
            OutgoingSeparator = outgoingSeparator;

            CommandDeleteItem = new DelegateCommand(ExecuteDeleteItem);
            CommandAddItem = new DelegateCommand(ExecuteAddItem);
            CommandNullOutEntry = new DelegateCommand(NullOutRecord);
        }


        public GuiMetaDataModel(DateTime? extractionDate)
        {
            ExtractionDate = extractionDate;
            CommandDeleteItem = new DelegateCommand(ExecuteDeleteItem);
            CommandAddItem = new DelegateCommand(ExecuteAddItem);
            CommandNullOutEntry = new DelegateCommand(NullOutRecord);
        }


        public GuiMetaDataModel(string strArg, GuiObjectType guiObjectType)
        {

            if (guiObjectType == GuiObjectType.comment)
            {
                Comment = strArg;

            }
            else if (guiObjectType == GuiObjectType.history)
            {
                History = strArg;
                // Start disabled for [0-1] multiplicity
                ExecuteDeleteItem();
            }

            CommandDeleteItem = new DelegateCommand(ExecuteDeleteItem);
            CommandAddItem = new DelegateCommand(ExecuteAddItem);
            CommandNullOutEntry = new DelegateCommand(NullOutRecord);
        }



        public void ExecuteDeleteItem()
        {
            IsDeleted = true;
            VisibilityItem = Visibility.Collapsed;
            VisibilityAddItem = Visibility.Visible;
        }

        public void ExecuteAddItem()
        {
            IsDeleted = false;
            _ResetAllDataFields();
            VisibilityItem = Visibility.Visible;
            VisibilityAddItem = Visibility.Hidden;
        }


        public void SetDeleteButtonVisible()
        {
            DeleteButtonVisibility = Visibility.Visible;
        }

        public void SetDeleteButtonHidden()
        {
            DeleteButtonVisibility = Visibility.Hidden;
        }

        public void NullOutRecord()
        {
            _ResetAllDataFields();
        }


        private void _ResetAllDataFields()
        {
            Comment = string.Empty;
            Email = string.Empty;
            Telephone = string.Empty;
            ContactPerson = string.Empty;
            Entity = string.Empty;
            SystemName = string.Empty;
            SystemVersion = string.Empty;
            SystemType = string.Empty;
            SystemTypeVersion = string.Empty;
            ArchiveDescription = string.Empty;
            AgreementNumber = string.Empty;
            History = string.Empty;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            ExtractionDate = DateTime.Today;
            IncommingSeparator = string.Empty;
            OutgoingSeparator = string.Empty;
        }
    }


    public enum GuiObjectType
    {
        archiveDescription,
        entity,
        system,
        comment,
        history,
        archiveData,
        noarkObligatory
    }

}