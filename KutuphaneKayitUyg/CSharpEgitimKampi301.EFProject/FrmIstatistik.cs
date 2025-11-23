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
    public partial class FrmIstatistik : Form
    {
        public FrmIstatistik()
        {
            InitializeComponent();
        }
        LibraNexusEntities  db = new LibraNexusEntities();
        private void FrmIstatistik_Load(object sender, EventArgs e)
        {
            //toplam admin
            var adminCount = db.Admin.Count();
            lblTotalAdmin.Text = adminCount.ToString();
            //toplam kullanıcı
            var userCount = db.User.Count();
            lblTotalUser.Text = userCount.ToString();
            //toplam kitap
            var bookCount = db.Book.Count();
            lblTotalBook.Text = bookCount.ToString();
            //toplam tür
            var genreCount = db.Genre.Count();
            lblTotalGenre.Text = genreCount.ToString();
            //toplam yazar
            var authorCount = db.Author.Count();
            lblTotalAuthor.Text = authorCount.ToString();
            //en çok kitabı olan yazar
            // 1. Null kontrolü ile güvenli versiyon
            var mostFrequentAuthorId = db.Book
                .GroupBy(b => b.AuthorId)
                .Select(g => new { AuthorId = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault()?.AuthorId;

            if (mostFrequentAuthorId.HasValue)
            {
                var author = db.Author
                    .Where(a => a.AuthorId == mostFrequentAuthorId.Value)
                    .Select(a => new { a.AuthorName, a.AuthorSurname })
                    .FirstOrDefault();

                if (author != null)
                {
                    lblMostBookAuthor.Text = $"{author.AuthorName} {author.AuthorSurname}";
                }
                else
                {
                    lblMostBookAuthor.Text = "Yazar bulunamadı";
                }
            }
            else
            {
                lblMostBookAuthor.Text = "Kayıt bulunamadı";
            }
            //en çok kitabı olan tür
            var mostFrequentGenreId = db.Genre
               .GroupBy(b => b.GenreId)
               .Select(g => new { GenreId = g.Key, Count = g.Count() })
               .OrderByDescending(x => x.Count)
               .FirstOrDefault()?.GenreId;

            if (mostFrequentGenreId.HasValue)
            {
                var genre = db.Genre
                    .Where(a => a.GenreId == mostFrequentGenreId.Value)
                    .Select(a => new { a.GenreTitle })
                    .FirstOrDefault();

                if (genre != null)
                {
                    lblMostBookGenre.Text = $"{genre.GenreTitle}";
                }
                else
                {
                    lblMostBookGenre.Text = "Yazar bulunamadı";
                }
            }
            else
            {
                lblMostBookGenre.Text = "Kayıt bulunamadı";
            }
            //kütüphanede olan toplam kitap sayısı
            var totalLibBook = db.Book.Count(x=> x.IsInLibrary == true);
            lblTotalBookLibrary.Text = totalLibBook.ToString();
            // ödünç alınan toplam kitap sayısı
            var totalBorrowBook = db.Book.Count(x => x.IsInLibrary == true);
            lblTotalBookBorrowed.Text = totalBorrowBook.ToString();
            //geciken kitap sayısı
            var totalDelayedBook = db.Borrowing.Count(x => x.IsItDelayed == true);
            lblTotalBookDelayed.Text = totalDelayedBook.ToString();
            //en son eklenen kitap
            var maxId = db.Book.Max(b => b.BookId);
            var lastBook = db.Book.Find(maxId);
            var lastBookTitle = db.Book.Find(db.Book.Max(b => b.BookId))?.BookTitle;
            lblLastBook.Text = lastBookTitle.ToString();
            //toplam para cezası
            var totalPenalty = db.Borrowing.Sum(b => b.TotalPenalty);
            lblTotalPenalty.Text = totalPenalty.ToString() + "TL";
        }
    }
}
