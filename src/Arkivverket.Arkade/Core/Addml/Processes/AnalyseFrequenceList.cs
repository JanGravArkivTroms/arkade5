﻿using System.Collections.Generic;
using System.Numerics;
using Arkivverket.Arkade.Core.Addml.Definitions;
using Arkivverket.Arkade.Resources;
using Arkivverket.Arkade.Tests;
using Arkivverket.Arkade.Util;

namespace Arkivverket.Arkade.Core.Addml.Processes
{
    public class AnalyseFrequenceList : AddmlProcess
    {
        private readonly TestId _id = new TestId(TestId.TestKind.Addml, 10);

        public const string Name = "Analyse_FrequenceList";

        private readonly Dictionary<FieldIndex, FrequencyList> _frequenceListPerField
            = new Dictionary<FieldIndex, FrequencyList>();

        private readonly List<TestResult> _testResults = new List<TestResult>();

        public override TestId GetId()
        {
            return _id;
        }

        public override string GetName()
        {
            return Name;
        }

        public override string GetDescription()
        {
            return Messages.AnalyseFrequenceListDescription;
        }

        public override TestType GetTestType()
        {
            return TestType.ContentAnalysis;
        }

        protected override List<TestResult> GetTestResults()
        {
            return _testResults;
        }

        protected override void DoRun(FlatFile flatFile)
        {
        }

        protected override void DoRun(Record record)
        {
        }

        protected override void DoEndOfFile()
        {
            foreach (KeyValuePair<FieldIndex, FrequencyList> entry in _frequenceListPerField)
            {
                FieldIndex index = entry.Key;
                FrequencyList frequencyList = entry.Value;

                foreach (KeyValuePair<string, BigInteger> e in frequencyList.Get())
                {
                    string word = e.Key;
                    BigInteger count = e.Value;

                    _testResults.Add(new TestResult(ResultType.Success, AddmlLocation.FromFieldIndex(index),
                        string.Format(Messages.AnalyseFrequenceListMessage, count, word)));
                }
            }

            _frequenceListPerField.Clear();
        }

        protected override void DoRun(Field field)
        {
            FieldIndex fieldIndeks = field.Definition.GetIndex();
            if (!_frequenceListPerField.ContainsKey(fieldIndeks))
            {
                _frequenceListPerField.Add(fieldIndeks, new FrequencyList());
            }

            FrequencyList frequencyList = _frequenceListPerField[fieldIndeks];
            frequencyList.Add(field.Value);
        }
    }
}