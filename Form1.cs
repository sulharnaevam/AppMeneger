using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//посмотреть где я несколько раз изменяла и исправить это
//ПОСМОТРЕТЬ НАЗВАНИЯ, КОТОРЫЕ Я ИЗМЕНЯЛА 
namespace AppMeneger
{
    public partial class Form1 : Form
    {
        private List<Note> notes;
        public Form1()
        {
            InitializeComponent();
            LoadData();
            Logger.Log("Запуск приложения");
        } 
        public void LoadData()
        {
            notes = DataStorage.Load();
            RefreshGrid();
            Logger.Log($"Загрузка данных из файла. Загружено {notes.Count} заметок");
        }

        public void SaveData()
        {
            DataStorage.Save(notes);
        }
        public void RefreshGrid()
        {
            var filtered = notes.AsEnumerable();

            if (cmbPriorityFilter.SelectedItem?.ToString() != "Все")
                filtered = filtered.Where(n => n.Prioritet == cmbPriorityFilter.SelectedItem?.ToString());

            if (check.Checked)
                filtered = filtered.Where(n => !n.IsCompleted);

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                filtered = filtered.Where(n => n.Name.ToLower().Contains(txtSearch.Text.ToLower()));

            var list = filtered.ToList();

            UpdateButtonState();
        }

        public void UpdateButtonState()
        {
            bool hasSelection = dgvNotes.SelectedRows.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            var completed = notes.Where(n => n.IsCompleted).ToList();
            if (completed.Count == 0)
            {
                MessageBox.Show("Нет выполненных заметок");
                return;
            }
            if (MessageBox.Show($"Удалить {completed.Count} выполненных заметок?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                notes.RemoveAll(n => n.IsCompleted);
                SaveData();
                RefreshGrid();
                Logger.Log($"Удалено выполненных заметок: {completed.Count}");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateButtonState();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvNotes.SelectedRows.Count == 0) return;
            var note = (Note)dgvNotes.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show($"Удалить заметку \"{note.Name}\"?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                notes.RemoveAll(n => n.Id == note.Id);
                SaveData();
                RefreshGrid();
                Logger.Log($"Удалена заметка: {note.Name}");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvNotes.SelectedRows.Count == 0) return;
            var oldNote = (Note)dgvNotes.SelectedRows[0].DataBoundItem;
            var form = new Form2(oldNote);
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Note.Id = oldNote.Id;
                int index = notes.FindIndex(n => n.Id == oldNote.Id);
                notes[index] = form.Note;
                SaveData();
                RefreshGrid();
                Logger.Log($"Редактирована заметка: {oldNote.Name} -> {form.Note.Name}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new Form2(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Note.Id = notes.Count > 0 ? notes.Max(n => n.Id) + 1 : 1;
                notes.Add(form.Note);
                SaveData();
                RefreshGrid();
                Logger.Log($"Добавлена заметка: {form.Note.Name}");
            }
        }
    }
}
