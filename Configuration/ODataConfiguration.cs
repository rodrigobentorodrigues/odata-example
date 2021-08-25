using BookStore.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace BookStore.Configuration
{
    public static class ODataConfiguration
    {

        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books");
            builder.EntitySet<Press>("Presses");
            return builder.GetEdmModel();
        }

    }
}
