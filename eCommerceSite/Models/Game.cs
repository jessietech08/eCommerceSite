using System.ComponentModel.DataAnnotations;

namespace eCommerceSite.Models
{
    /// <summary>
    /// Represents a single game for available for purchase
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The unique identifier for each game product
        /// </summary>
        [Key]
        public int GameId { get; set; }

        /// <summary>
        /// The official title of the video game
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The sales price
        /// </summary>
        [Range(0, 1000)]
        public double Price { get; set; }

        // Todo: add rating


    }
}
