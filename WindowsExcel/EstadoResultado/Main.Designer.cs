namespace EstadoResultado
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstInputFiles = new System.Windows.Forms.ListBox();
            this.dgFolderSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.btInFolder = new System.Windows.Forms.Button();
            this.lbInputPath = new System.Windows.Forms.Label();
            this.btOutputPath = new System.Windows.Forms.Button();
            this.lbOutputPath = new System.Windows.Forms.Label();
            this.btProcesar = new System.Windows.Forms.Button();
            this.lbArchivoDeSalida = new System.Windows.Forms.Label();
            this.lbOutputFile = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.datosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineasDeEstadoResultadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.areasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carpetaDeEntradaPorDefectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carpetaDeSalidaPorDefectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carpetaDeInicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatoDeSalidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manteciónCódigosDeEERRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mantenciónDeCódigosDeAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstInputFiles
            // 
            this.lstInputFiles.FormattingEnabled = true;
            this.lstInputFiles.Location = new System.Drawing.Point(3, 124);
            this.lstInputFiles.Name = "lstInputFiles";
            this.lstInputFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstInputFiles.Size = new System.Drawing.Size(267, 212);
            this.lstInputFiles.TabIndex = 0;
            this.lstInputFiles.SelectedIndexChanged += new System.EventHandler(this.lstInputFiles_SelectedIndexChanged);
            // 
            // btInFolder
            // 
            this.btInFolder.AutoSize = true;
            this.btInFolder.Location = new System.Drawing.Point(3, 3);
            this.btInFolder.Name = "btInFolder";
            this.btInFolder.Size = new System.Drawing.Size(100, 23);
            this.btInFolder.TabIndex = 1;
            this.btInFolder.Text = "Carpeta Entrada";
            this.btInFolder.UseVisualStyleBackColor = true;
            this.btInFolder.Click += new System.EventHandler(this.btInFolder_Click);
            // 
            // lbInputPath
            // 
            this.lbInputPath.AutoSize = true;
            this.lbInputPath.Location = new System.Drawing.Point(103, 11);
            this.lbInputPath.Name = "lbInputPath";
            this.lbInputPath.Size = new System.Drawing.Size(0, 13);
            this.lbInputPath.TabIndex = 2;
            // 
            // btOutputPath
            // 
            this.btOutputPath.AutoSize = true;
            this.btOutputPath.Location = new System.Drawing.Point(3, 32);
            this.btOutputPath.Name = "btOutputPath";
            this.btOutputPath.Size = new System.Drawing.Size(101, 23);
            this.btOutputPath.TabIndex = 3;
            this.btOutputPath.Text = "Carpeta de Salida";
            this.btOutputPath.UseVisualStyleBackColor = true;
            this.btOutputPath.Click += new System.EventHandler(this.btOutputFolder_Click);
            // 
            // lbOutputPath
            // 
            this.lbOutputPath.AutoSize = true;
            this.lbOutputPath.Location = new System.Drawing.Point(110, 42);
            this.lbOutputPath.Name = "lbOutputPath";
            this.lbOutputPath.Size = new System.Drawing.Size(0, 13);
            this.lbOutputPath.TabIndex = 4;
            // 
            // btProcesar
            // 
            this.btProcesar.Location = new System.Drawing.Point(9, 87);
            this.btProcesar.Name = "btProcesar";
            this.btProcesar.Size = new System.Drawing.Size(95, 31);
            this.btProcesar.TabIndex = 5;
            this.btProcesar.Text = "Procesar";
            this.btProcesar.UseVisualStyleBackColor = true;
            this.btProcesar.Click += new System.EventHandler(this.btProcesar_Click);
            // 
            // lbArchivoDeSalida
            // 
            this.lbArchivoDeSalida.AutoSize = true;
            this.lbArchivoDeSalida.Location = new System.Drawing.Point(6, 67);
            this.lbArchivoDeSalida.Name = "lbArchivoDeSalida";
            this.lbArchivoDeSalida.Size = new System.Drawing.Size(91, 13);
            this.lbArchivoDeSalida.TabIndex = 6;
            this.lbArchivoDeSalida.Text = "Archivo de salida:";
            // 
            // lbOutputFile
            // 
            this.lbOutputFile.AutoSize = true;
            this.lbOutputFile.Location = new System.Drawing.Point(103, 67);
            this.lbOutputFile.Name = "lbOutputFile";
            this.lbOutputFile.Size = new System.Drawing.Size(0, 13);
            this.lbOutputFile.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datosToolStripMenuItem,
            this.configuraciónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(561, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // datosToolStripMenuItem
            // 
            this.datosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemsToolStripMenuItem,
            this.lineasDeEstadoResultadoToolStripMenuItem,
            this.areasToolStripMenuItem});
            this.datosToolStripMenuItem.Name = "datosToolStripMenuItem";
            this.datosToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.datosToolStripMenuItem.Text = "Datos";
            // 
            // itemsToolStripMenuItem
            // 
            this.itemsToolStripMenuItem.Name = "itemsToolStripMenuItem";
            this.itemsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.itemsToolStripMenuItem.Text = "Items";
            this.itemsToolStripMenuItem.Click += new System.EventHandler(this.itemsToolStripMenuItem_Click);
            // 
            // lineasDeEstadoResultadoToolStripMenuItem
            // 
            this.lineasDeEstadoResultadoToolStripMenuItem.Name = "lineasDeEstadoResultadoToolStripMenuItem";
            this.lineasDeEstadoResultadoToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.lineasDeEstadoResultadoToolStripMenuItem.Text = "Lineas de estado resultado";
            this.lineasDeEstadoResultadoToolStripMenuItem.Click += new System.EventHandler(this.lineasDeEstadoResultadoToolStripMenuItem_Click);
            // 
            // areasToolStripMenuItem
            // 
            this.areasToolStripMenuItem.Name = "areasToolStripMenuItem";
            this.areasToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.areasToolStripMenuItem.Text = "Areas";
            this.areasToolStripMenuItem.Click += new System.EventHandler(this.areasToolStripMenuItem_Click);
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carpetaDeEntradaPorDefectoToolStripMenuItem,
            this.carpetaDeSalidaPorDefectoToolStripMenuItem,
            this.carpetaDeInicioToolStripMenuItem,
            this.formatoDeSalidaToolStripMenuItem,
            this.manteciónCódigosDeEERRToolStripMenuItem,
            this.mantenciónDeCódigosDeAreaToolStripMenuItem});
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            // 
            // carpetaDeEntradaPorDefectoToolStripMenuItem
            // 
            this.carpetaDeEntradaPorDefectoToolStripMenuItem.Name = "carpetaDeEntradaPorDefectoToolStripMenuItem";
            this.carpetaDeEntradaPorDefectoToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.carpetaDeEntradaPorDefectoToolStripMenuItem.Text = "Carpeta de entrada por defecto";
            // 
            // carpetaDeSalidaPorDefectoToolStripMenuItem
            // 
            this.carpetaDeSalidaPorDefectoToolStripMenuItem.Name = "carpetaDeSalidaPorDefectoToolStripMenuItem";
            this.carpetaDeSalidaPorDefectoToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.carpetaDeSalidaPorDefectoToolStripMenuItem.Text = "Carpeta de salida por defecto";
            // 
            // carpetaDeInicioToolStripMenuItem
            // 
            this.carpetaDeInicioToolStripMenuItem.Name = "carpetaDeInicioToolStripMenuItem";
            this.carpetaDeInicioToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.carpetaDeInicioToolStripMenuItem.Text = "Carpeta de inicio";
            // 
            // formatoDeSalidaToolStripMenuItem
            // 
            this.formatoDeSalidaToolStripMenuItem.Name = "formatoDeSalidaToolStripMenuItem";
            this.formatoDeSalidaToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.formatoDeSalidaToolStripMenuItem.Text = "Formato de salida";
            // 
            // manteciónCódigosDeEERRToolStripMenuItem
            // 
            this.manteciónCódigosDeEERRToolStripMenuItem.Name = "manteciónCódigosDeEERRToolStripMenuItem";
            this.manteciónCódigosDeEERRToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.manteciónCódigosDeEERRToolStripMenuItem.Text = "Manteción códigos de EERR";
            this.manteciónCódigosDeEERRToolStripMenuItem.Click += new System.EventHandler(this.manteciónCódigosDeEERRToolStripMenuItem_Click);
            // 
            // mantenciónDeCódigosDeAreaToolStripMenuItem
            // 
            this.mantenciónDeCódigosDeAreaToolStripMenuItem.Name = "mantenciónDeCódigosDeAreaToolStripMenuItem";
            this.mantenciónDeCódigosDeAreaToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.mantenciónDeCódigosDeAreaToolStripMenuItem.Text = "Mantención de códigos de Area";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btInFolder);
            this.panel1.Controls.Add(this.lbInputPath);
            this.panel1.Controls.Add(this.lbOutputFile);
            this.panel1.Controls.Add(this.lstInputFiles);
            this.panel1.Controls.Add(this.lbArchivoDeSalida);
            this.panel1.Controls.Add(this.btOutputPath);
            this.panel1.Controls.Add(this.btProcesar);
            this.panel1.Controls.Add(this.lbOutputPath);
            this.panel1.Location = new System.Drawing.Point(12, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(538, 343);
            this.panel1.TabIndex = 9;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 384);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Caraga de datos";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstInputFiles;
        private System.Windows.Forms.FolderBrowserDialog dgFolderSelect;
        private System.Windows.Forms.Button btInFolder;
        private System.Windows.Forms.Label lbInputPath;
        private System.Windows.Forms.Button btOutputPath;
        private System.Windows.Forms.Label lbOutputPath;
        private System.Windows.Forms.Button btProcesar;
        private System.Windows.Forms.Label lbArchivoDeSalida;
        private System.Windows.Forms.Label lbOutputFile;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem datosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineasDeEstadoResultadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem areasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carpetaDeEntradaPorDefectoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carpetaDeSalidaPorDefectoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carpetaDeInicioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatoDeSalidaToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem manteciónCódigosDeEERRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mantenciónDeCódigosDeAreaToolStripMenuItem;
    }
}

