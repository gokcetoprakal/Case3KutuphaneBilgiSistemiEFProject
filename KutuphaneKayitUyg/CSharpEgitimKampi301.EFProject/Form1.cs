using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi301.EFProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        LibraNexusEntities db = new LibraNexusEntities();
        Book book = new Book();
        private void btnList_Click(object sender, EventArgs e)
        {
            var values = db.Book.ToList();
            dataGridView1.DataSource = values;  
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            book.BookTitle = txtTitle.Text;
            book.AuthorId = int.Parse(txtAuthorId.Text);
            book.GenreId = int.Parse(txtGenreId.Text);
            book.BookPublishYear = int.Parse(txtPublishYear.Text);
            book.BookLanguage = txtLanguage.Text;
            book.BookTotalCopy = int.Parse(txtTotalCopy.Text);
            book.BookYear = int.Parse(txtYear.Text);
            book.IsInLibrary = Convert.ToBoolean(chcIsInLib.Checked);
            db.Book.Add(book);
            db.SaveChanges();
            MessageBox.Show("Yeni kitap bilgisi başarıyla eklendi.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            var removeValue = db.Book.Find(id);
            db.Book.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Kitap bilgisi başarıyla silindi.");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            var updateValue = db.Book.Find(id);
            updateValue.BookTitle = txtTitle.Text;
            db.SaveChanges();
        }

        private void btnGetByID_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            var values = db.Book.Where(x=>x.BookId == id).ToList();
            dataGridView1.DataSource = values;
        }
    }
}
