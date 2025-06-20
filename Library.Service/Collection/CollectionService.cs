using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    public class CollectionService: ICollectionService {


        private readonly ApplicationDbContext _context;

        private readonly IDiscardService _discardService;

        private readonly IDonationService _donationService;

        private readonly ILoanService _loanService;

        private List<Guid>? borrowedBooksIds;

        private List<Guid>? donatedBooksIds;

        private List<Guid>? discardedBooksIds;


        public CollectionService(ApplicationDbContext context, IDiscardService discardService,
        ILoanService loanService, IDonationService donationService) {
            _context = context;
            _discardService = discardService;
            _loanService = loanService;
            _donationService = donationService;
            FillBooksLists().Wait();
        }


        private async Task FillBooksLists() {

            var borrowedBooksIdsResp = await _loanService.GetBorrowedBooksIds();
            var donatedBooksIdsResp = await _donationService.GetDonatedBooksIds();
            var discardedBooksIdsResp = await _discardService.GetDiscardedBooksIds();

            borrowedBooksIds = borrowedBooksIdsResp.Data;
            donatedBooksIds = donatedBooksIdsResp.Data;
            discardedBooksIds = discardedBooksIdsResp.Data;

        }


        public async Task<Response<List<BookModel>>> GetAvailableBooks() {

            Response<List<BookModel>> response = new();

            try {

                List<BookModel> availableBooks = await _context.Books
                .Where(b =>
                    !discardedBooksIds!.Contains(b.Id) &&
                    !donatedBooksIds!.Contains(b.Id) &&
                    !borrowedBooksIds!.Contains(b.Id) &&
                    b.IsDeleted == false
                )
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .AsNoTracking()
                .ToListAsync();

                response.Data = availableBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<List<BookModel>>> GetCollectionBooks() {

            Response<List<BookModel>> response = new();

            try {

                List<BookModel> availableBooks = await _context.Books
                .Where(b =>
                    !discardedBooksIds!.Contains(b.Id) &&
                    !donatedBooksIds!.Contains(b.Id) &&
                    b.IsDeleted == false
                )
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .AsNoTracking()
                .ToListAsync();

                foreach (var book in availableBooks) {
                    book.IsBorrowed = borrowedBooksIds!.Contains(book.Id);
                }

                response.Data = availableBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<List<BookModel>>> GetCollectionBooksWithThumbnails() {

            Response<List<BookModel>> response = new();

            try {

                List<BookModel> availableBooks = await _context.Books
                .Select(static b => new BookModel {

                    Id = b.Id,
                    Title = b.Title,
                    Subtitle = b.Subtitle,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    Isbn = b.Isbn,
                    Edition = b.Edition,
                    Volume = b.Volume,
                    ReleaseYear = b.ReleaseYear,
                    AcquisitionDate = b.AcquisitionDate,
                    NumberOfPages = b.NumberOfPages,
                    RegistrationDate = b.RegistrationDate,
                    LastUpdateDate = b.LastUpdateDate,
                    Summary = b.Summary,
                    IsDeleted = b.IsDeleted,
                    IsBorrowed = b.IsBorrowed,
                    IsDiscarded = b.IsDiscarded,
                    IsDonated = b.IsDonated,

                    Cover = new CoverModel {
                        Id = b.Cover.Id,
                        Data = "",
                        Thumbnail = b.Cover.Thumbnail,
                        RegistrationDate = b.Cover.RegistrationDate,
                        LastUpdateDate = b.Cover.LastUpdateDate
                    }

                })
                .Where(b =>
                    !discardedBooksIds!.Contains(b.Id) &&
                    !donatedBooksIds!.Contains(b.Id) &&
                    b.IsDeleted == false
                )
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .AsNoTracking()
                .ToListAsync();

                foreach (var book in availableBooks) {
                    book.IsBorrowed = borrowedBooksIds!.Contains(book.Id);
                }

                response.Data = availableBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


    }


}
