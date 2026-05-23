using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AppMeneger
{
    public partial class Form2 : Form
    {
        public Note Note { get; set; }
        private Note editNote;

        public Form2(Note noteToEdit)
        {
            InitializeComponent();

            if (noteToEdit != null)
            {
                editNote = noteToEdit;
                Note = new Note
                {
                    Id = editNote.Id,
                    Name = editNote.Name,
                    Content = editNote.Content,
                    Data = editNote.Data,
                    Prioritet = editNote.Prioritet,
                    IsCompleted = editNote.IsCompleted
                };
                this.Text = "Редактирование заметки";
            }
            else
            {
                Note = new Note();
                this.Text = "Новая заметка";
            }

            LoadNoteToForm();
        }

        private void LoadNoteToForm()
        {
            txtName.Text = Note.Name;
            cmbPrioritet.SelectedItem = Note.Prioritet;
            check.Checked = Note.IsCompleted;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Название заметки обязательно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            Note.Name = txtName.Text;
            Note.Prioritet = cmbPrioritet.SelectedItem?.ToString() ?? "Средний";
            Note.IsCompleted = check.Checked;
        }
    }
}