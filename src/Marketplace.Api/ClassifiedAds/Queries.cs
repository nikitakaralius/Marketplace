using System.Data.Common;
using Dapper;
using Marketplace.Domain.ClassifiedAd;
using static Marketplace.ClassifiedAds.QueryModels;
using static Marketplace.ClassifiedAds.ReadModels;

namespace Marketplace.ClassifiedAds;

internal static class Queries
{
    public static async Task<IEnumerable<ClassifiedAdListItem>> QueryAsync(
        this DbConnection connection,
        GetPublishedClassifiedAds query)
    {
        const string sql = """
        SELECT "DatabaseId", "Price_Amount", "Title_Value"
        FROM "ClassifiedAds"
        WHERE "State"=@State LIMIT
        @PageSize OFFSET @Offset
        """;

        var request =connection.QueryAsync<ClassifiedAdListItem>(sql, new
        {
            State = (int) ClassifiedAd.AdState.Active,
            PageSize = query.PageSize,
            Offset = Offset(query.Page, query.PageSize)
        });

        return await request;
    }

    private static int Offset(int page, int pageSize) => page * pageSize;
}
