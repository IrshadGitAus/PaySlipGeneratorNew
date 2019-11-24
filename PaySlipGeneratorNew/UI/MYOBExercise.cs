using PaySlipGeneratorNew.Core.Model;
using PaySlipGeneratorNew.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaySlipGeneratorNew.UI
{
    public partial class MYOBExercise: Form
    {
        private OpenFileDialog openFileDialog;
        private Label labelErrorMessage;
        private Button buttonFileBrowse;

        public MYOBExercise()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.buttonFileBrowse = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonFileBrowse
            // 
            this.buttonFileBrowse.Location = new System.Drawing.Point(13, 46);
            this.buttonFileBrowse.Name = "buttonFileBrowse";
            this.buttonFileBrowse.Size = new System.Drawing.Size(143, 50);
            this.buttonFileBrowse.TabIndex = 0;
            this.buttonFileBrowse.Text = "Browse a csv or a dat file";
            this.buttonFileBrowse.UseVisualStyleBackColor = true;
            this.buttonFileBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.Location = new System.Drawing.Point(5, 5);
            this.labelErrorMessage.Margin = new System.Windows.Forms.Padding(5);
            this.labelErrorMessage.MinimumSize = new System.Drawing.Size(455, 0);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(455, 13);
            this.labelErrorMessage.TabIndex = 1;
            // 
            // MYOBExercise
            // 
            this.ClientSize = new System.Drawing.Size(480, 261);
            this.Controls.Add(this.labelErrorMessage);
            this.Controls.Add(this.buttonFileBrowse);
            this.Name = "MYOBExercise";
            this.Load += new System.EventHandler(this.MYOBExercise_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a file to import.";
                openFileDialog.Filter = "CSV or Data files|*.dat;*.csv";
                try
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileExtension = Path.GetExtension(openFileDialog.FileName);
                        var fileExtensionType = GetFileExtensionType(fileExtension);
                        //collect the selected file in a stream
                        using (var fileStream = new StreamReader(openFileDialog.OpenFile()))
                        {
                            //fileStream
                            EmployeePaySlipService employeePaySlipService = new EmployeePaySlipService();
                            var _outputFileWriter = new OutputWriter();

                            var employeesMonthlyPaySlip = employeePaySlipService.GetEmployeesPaySlip(fileStream, fileExtensionType);

                            if (employeesMonthlyPaySlip != null && employeesMonthlyPaySlip.Any())
                            {
                                _outputFileWriter.Write(employeesMonthlyPaySlip);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
        }

        private FileExtensionType GetFileExtensionType(string fileExtension)
        {
            switch(fileExtension.ToUpperInvariant())
            {
                case ".CSV":
                    return FileExtensionType.CSV;
                case ".DAT":
                    return FileExtensionType.DAT;
                default:
                    return FileExtensionType.OTHER;
            }

        }

        private void ShowErrorMessage(string message)
        {
            labelErrorMessage.Text = message;
        }

        private void ResetControls()
        {
            labelErrorMessage.Text = string.Empty;
        }

        private void MYOBExercise_Load(object sender, EventArgs e)
        {

        }
    }
}
