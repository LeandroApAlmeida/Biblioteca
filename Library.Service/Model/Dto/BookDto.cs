namespace Library.Services.Model.Dto {
    
    
    public class BookDto {

        public Guid Id { get; set; }

        public CoverDto? Cover { get; set; }

        public required string Title { get; set; }

        public string? Subtitle { get; set; }

        public required string Author { get; set; }

        public required string Publisher { get; set; }

        public required string Isbn { get; set; }

        public required int Edition { get; set; }

        public required int Volume { get; set; }

        public required int ReleaseYear { get; set; }

        public required int NumberOfPages { get; set; }

        public string? Summary { get; set; }

        public required DateTime AcquisitionDate { get; set; }

        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public required bool IsDeleted { get; set; } = false;

        public bool IsBorrowed { get; set; } = false;

        public bool IsDonated { get; set; } = false;

        public bool IsDiscarded { get; set; } = false;

    
    }


}
