namespace eCommerceSite.Models
{
    public class GameCatalogViewModel
    {
        public GameCatalogViewModel(List<Game> games, int lastPage, int currPage)
        {
            Games = games;
            LastPage = lastPage;
            CurrentPage = currPage;
        }

        public List<Game> Games { get; private set; }

        /// <summary>
        /// The last page of the catalog. Calculated by
        /// total number of products divided by 
        /// products per page
        /// </summary>
        public int LastPage { get; private set; }

        /// <summary>
        /// The currentr page user is viewing
        /// </summary>
        public int CurrentPage { get; private set; }
    }
}
