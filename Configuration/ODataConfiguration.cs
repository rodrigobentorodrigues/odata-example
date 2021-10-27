using BookStore.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace BookStore.Configuration
{
    public static class ODataConfiguration
    {

        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>(nameof(Book));
            builder.EntitySet<Press>(nameof(Press));
            return builder.GetEdmModel();
        }

    }
}
