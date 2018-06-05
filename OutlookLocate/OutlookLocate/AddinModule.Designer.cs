namespace OutlookLocate
{
    partial class AddinModule
    {
        /// <summary>
        /// Required by designer
        /// </summary>
        private System.ComponentModel.IContainer components;
 
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required by designer support - do not modify
        /// the following method
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddinModule));
            this.adxLocateRibbonTab = new AddinExpress.MSO.ADXRibbonTab(this.components);
            this.adxRibbonGroup1 = new AddinExpress.MSO.ADXRibbonGroup(this.components);
            this.adxRBUpload = new AddinExpress.MSO.ADXRibbonButton(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.adxLocateSettings = new AddinExpress.MSO.ADXRibbonDialogBoxLauncher(this.components);
            this.OutlookEvents = new AddinExpress.MSO.ADXOutlookAppEvents(this.components);
            // 
            // adxLocateRibbonTab
            // 
            this.adxLocateRibbonTab.Caption = "Outlook Locate";
            this.adxLocateRibbonTab.Controls.Add(this.adxRibbonGroup1);
            this.adxLocateRibbonTab.Id = "adxRibbonTab_150018dfd540413193f95b82303be320";
            this.adxLocateRibbonTab.IdMso = "TabSendReceive";
            this.adxLocateRibbonTab.Ribbons = ((AddinExpress.MSO.ADXRibbons)(((AddinExpress.MSO.ADXRibbons.msrOutlookMailRead | AddinExpress.MSO.ADXRibbons.msrOutlookMailCompose) 
            | AddinExpress.MSO.ADXRibbons.msrOutlookExplorer)));
            // 
            // adxRibbonGroup1
            // 
            this.adxRibbonGroup1.Caption = "Locate";
            this.adxRibbonGroup1.Controls.Add(this.adxRBUpload);
            this.adxRibbonGroup1.Controls.Add(this.adxLocateSettings);
            this.adxRibbonGroup1.Id = "adxRibbonGroup_676ddeacfab14e3db686c75fd8a2c930";
            this.adxRibbonGroup1.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.adxRibbonGroup1.Ribbons = ((AddinExpress.MSO.ADXRibbons)(((AddinExpress.MSO.ADXRibbons.msrOutlookMailRead | AddinExpress.MSO.ADXRibbons.msrOutlookMailCompose) 
            | AddinExpress.MSO.ADXRibbons.msrOutlookExplorer)));
            // 
            // adxRBUpload
            // 
            this.adxRBUpload.Caption = "Test Upload to Locate";
            this.adxRBUpload.Id = "adxRibbonButton_fb9da5a4323240fdbd41517b2b0b595f";
            this.adxRBUpload.Image = 0;
            this.adxRBUpload.ImageList = this.imageList;
            this.adxRBUpload.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.adxRBUpload.Ribbons = ((AddinExpress.MSO.ADXRibbons)(((AddinExpress.MSO.ADXRibbons.msrOutlookMailRead | AddinExpress.MSO.ADXRibbons.msrOutlookMailCompose) 
            | AddinExpress.MSO.ADXRibbons.msrOutlookExplorer)));
            this.adxRBUpload.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
            this.adxRBUpload.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.adxRBUpload_OnClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Optimise-Fill.ico");
            // 
            // adxLocateSettings
            // 
            this.adxLocateSettings.Id = "adxRibbonDialogBoxLauncher_f0f9235566014463b94eac89606dab36";
            this.adxLocateSettings.Ribbons = ((AddinExpress.MSO.ADXRibbons)(((AddinExpress.MSO.ADXRibbons.msrOutlookMailRead | AddinExpress.MSO.ADXRibbons.msrOutlookMailCompose) 
            | AddinExpress.MSO.ADXRibbons.msrOutlookExplorer)));
            // 
            // AddinModule
            // 
            this.AddinName = "OutlookLocate";
            this.SupportedApps = AddinExpress.MSO.ADXOfficeHostApp.ohaOutlook;

        }
        #endregion

        private AddinExpress.MSO.ADXRibbonTab adxLocateRibbonTab;
        private AddinExpress.MSO.ADXRibbonGroup adxRibbonGroup1;
        private AddinExpress.MSO.ADXRibbonButton adxRBUpload;
        private System.Windows.Forms.ImageList imageList;
        private AddinExpress.MSO.ADXRibbonDialogBoxLauncher adxLocateSettings;
        private AddinExpress.MSO.ADXOutlookAppEvents OutlookEvents;
    }
}

