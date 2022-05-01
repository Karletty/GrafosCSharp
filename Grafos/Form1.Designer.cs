namespace Grafos
{
    partial class Simulador
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Pizarra = new System.Windows.Forms.Panel();
            this.CMSCrearVertice = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoVerticeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CMSEliminarVertice = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarVérticeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CMSCrearVertice.SuspendLayout();
            this.CMSEliminarVertice.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pizarra
            // 
            this.Pizarra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.Pizarra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pizarra.Location = new System.Drawing.Point(0, 0);
            this.Pizarra.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Pizarra.Name = "Pizarra";
            this.Pizarra.Size = new System.Drawing.Size(881, 501);
            this.Pizarra.TabIndex = 0;
            this.Pizarra.Paint += new System.Windows.Forms.PaintEventHandler(this.Pizarra_Paint);
            this.Pizarra.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pizarra_MouseDown);
            this.Pizarra.MouseLeave += new System.EventHandler(this.Pizarra_MouseLeave);
            this.Pizarra.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pizarra_MouseMove);
            this.Pizarra.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Pizarra_MouseUp);
            // 
            // CMSCrearVertice
            // 
            this.CMSCrearVertice.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CMSCrearVertice.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoVerticeToolStripMenuItem});
            this.CMSCrearVertice.Name = "contextMenuStrip2";
            this.CMSCrearVertice.Size = new System.Drawing.Size(171, 28);
            // 
            // nuevoVerticeToolStripMenuItem
            // 
            this.nuevoVerticeToolStripMenuItem.Name = "nuevoVerticeToolStripMenuItem";
            this.nuevoVerticeToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.nuevoVerticeToolStripMenuItem.Text = "Nuevo Vértice";
            this.nuevoVerticeToolStripMenuItem.Click += new System.EventHandler(this.nuevoVerticeToolStripMenuItem_Click);
            // 
            // CMSEliminarVertice
            // 
            this.CMSEliminarVertice.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CMSEliminarVertice.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarVérticeToolStripMenuItem});
            this.CMSEliminarVertice.Name = "CMSEliminarVertice";
            this.CMSEliminarVertice.Size = new System.Drawing.Size(182, 28);
            // 
            // eliminarVérticeToolStripMenuItem
            // 
            this.eliminarVérticeToolStripMenuItem.Name = "eliminarVérticeToolStripMenuItem";
            this.eliminarVérticeToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.eliminarVérticeToolStripMenuItem.Text = "Eliminar Vértice";
            this.eliminarVérticeToolStripMenuItem.Click += new System.EventHandler(this.eliminarVérticeToolStripMenuItem_Click);
            // 
            // Simulador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 501);
            this.Controls.Add(this.Pizarra);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Simulador";
            this.Text = "Simulador de grafos";
            this.CMSCrearVertice.ResumeLayout(false);
            this.CMSEliminarVertice.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pizarra;
        private System.Windows.Forms.ContextMenuStrip CMSCrearVertice;
        private System.Windows.Forms.ToolStripMenuItem nuevoVerticeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip CMSEliminarVertice;
        private System.Windows.Forms.ToolStripMenuItem eliminarVérticeToolStripMenuItem;
    }
}

