﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace NIMod
{
    public partial class NIEnemyModel
    {
        /// <summary>
        /// model input class for NIEnemyModel.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"HP")]
            public float HP { get; set; }

            [ColumnName(@"Mana")]
            public float Mana { get; set; }

            [ColumnName(@"Defense")]
            public float Defense { get; set; }

            [ColumnName(@"EOC")]
            public bool EOC { get; set; }

            [ColumnName(@"Skel")]
            public bool Skel { get; set; }

            [ColumnName(@"WOF")]
            public bool WOF { get; set; }

            [ColumnName(@"Enemy")]
            public float Enemy { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for NIEnemyModel.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"HP")]
            public float HP { get; set; }

            [ColumnName(@"Mana")]
            public float Mana { get; set; }

            [ColumnName(@"Defense")]
            public float Defense { get; set; }

            [ColumnName(@"EOC")]
            public float[] EOC { get; set; }

            [ColumnName(@"Skel")]
            public float[] Skel { get; set; }

            [ColumnName(@"WOF")]
            public float[] WOF { get; set; }

            [ColumnName(@"Enemy")]
            public uint Enemy { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public float PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\My Games\\Terraria\\tModLoader\\ModSources\\NIMod\\NIEnemyModel.zip";

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}