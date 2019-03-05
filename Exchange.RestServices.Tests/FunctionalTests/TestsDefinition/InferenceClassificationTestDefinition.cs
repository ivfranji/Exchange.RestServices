namespace Exchange.RestServices.Tests.Functional.TestsDefinition
{
    using System.Collections.Generic;
    using Exchange;
    using Microsoft.OutlookServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test case definition for inference classification.
    /// </summary>
    internal static class InferenceClassificationTestDefinition
    {
        /// <summary>
        /// CRUD operations for inference classification.
        /// </summary>
        /// <param name="exchangeService"></param>
        public static void CreateReadUpdateDeleteInferenceClassificationOverride(ExchangeService exchangeService)
        {
            InferenceClassificationOverride inferenceClassification = new InferenceClassificationOverride(exchangeService);
            inferenceClassification.ClassifyAs = InferenceClassificationType.Focused;
            inferenceClassification.SenderEmailAddress = new EmailAddress()
            {
                Address = "a@b.hr"
            };

            Assert.IsNull(inferenceClassification.Id);
            inferenceClassification.Save();
            Assert.IsNotNull(inferenceClassification.Id);

            List<InferenceClassificationOverride> inferenceClassificationOverrides =
                exchangeService.GetInferenceClassificationOverrides();

            Assert.IsTrue(inferenceClassificationOverrides.Count == 1);
            Assert.AreEqual(
                inferenceClassificationOverrides[0].ClassifyAs,
                inferenceClassification.ClassifyAs);

            Assert.AreEqual(
                inferenceClassificationOverrides[0].SenderEmailAddress.Address,
                inferenceClassification.SenderEmailAddress.Address);

            inferenceClassification.ClassifyAs = InferenceClassificationType.Other;
            inferenceClassification.Update();

            inferenceClassificationOverrides =
                exchangeService.GetInferenceClassificationOverrides();

            Assert.IsTrue(inferenceClassificationOverrides.Count == 1);
            Assert.AreEqual(
                inferenceClassificationOverrides[0].ClassifyAs,
                InferenceClassificationType.Other);

            inferenceClassification.Delete();
            Assert.IsNull(inferenceClassification.Id);
            Assert.IsNull(inferenceClassification.SenderEmailAddress);
        }
    }
}
