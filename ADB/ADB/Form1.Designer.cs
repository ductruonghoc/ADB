using System.Data.Entity;
using System.Windows.Forms;

namespace ADB
{
    partial class Form1
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
            this.SuspendLayout(); // Suspend layout to improve performance during multiple changes

            

            // Set the name and text for the form
            this.Name = "Form1";
            this.Text = "Form1";

            // Set the background color of the form (optional)
            this.BackColor = System.Drawing.SystemColors.ButtonFace;

            // Ensure the form's client size matches the intended design size
            this.ClientSize = new System.Drawing.Size(1280, 800);  // Optional, usually controlled by Size

            this.AutoScaleDimensions = new System.Drawing.SizeF(1F, 1F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ResumeLayout(false); // Resume layout after all changes are complete
        }

        #endregion
        //public class MyDbContext : DbContext
        //{
        //    public DbSet<Customer> Customers { get; set; }

        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    {
        //        optionsBuilder.UseSqlServer("YourConnectionString");
        //    }
        //}

    
    }
}

