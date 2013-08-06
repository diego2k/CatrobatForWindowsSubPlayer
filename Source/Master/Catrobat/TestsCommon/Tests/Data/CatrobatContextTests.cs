﻿using System;
using Catrobat.Core;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.Core.Storage;

namespace Catrobat.TestsCommon.Tests.Data
{
  [TestClass]
  public class CatrobatContextTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }

    [TestMethod]
    public void InitializeCatrobatContextTest()
    {
        //TestHelper.InitializeAndClearCatrobatContext();
        //CatrobatContext.SetContextHolder(new ContextHolderTests(new CatrobatContext()));

        //const string newProjectName1 = "DefaultProject";
        //CatrobatContext.GetContext().CreateNewProject(newProjectName1);

        //Assert.AreEqual("DefaultProject", CatrobatContext.GetContext().LocalSettings.CurrentProjectName);

        //CatrobatContext.SetContextHolder(null);
    }

    [TestMethod]
    public void StoreLocalSettingsTest()
    {
        // TODO: fix test
        throw new NotImplementedException();

      var catrobatContext = new CatrobatContext();

      using (var storage = StorageSystem.GetStorage())
      {
        storage.DeleteFile(CatrobatContextBase.LocalSettingsFilePath);
      }

      //CatrobatContext.StoreLocalSettingsStatic();

      using (var storage = StorageSystem.GetStorage())
      {
          Assert.IsTrue(storage.FileExists(CatrobatContextBase.LocalSettingsFilePath));
      }
    }
  }
}
