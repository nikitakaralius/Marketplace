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
        SELECT "DatabaseId" id, "Price_Amount" price, "Title_Value" title, "Price_Currency_Code" currencyCode
        FROM "ClassifiedAds"
        WHERE "State"=@State
        LIMIT @PageSize OFFSET @Offset
        """;

        var request = connection.QueryAsync<ClassifiedAdListItem>(sql, new
        {
            State = (int) ClassifiedAd.AdState.Active,
            PageSize = query.PageSize,
            Offset = Offset(query.Page, query.PageSize)
        });

        return await request;
    }

    public static async Task<IEnumerable<ClassifiedAdListItem>> QueryAsync(
        this DbConnection connection,
        GetOwnersClassifiedAds query)
    {
        const string sql = """
        SELECT "DatabaseId" id, "Price_Amount" price, "Title_Value" title
        FROM "ClassifiedAds"
        WHERE "OwnerId_Value"=@OwnerId
        LIMIT @PageSize
        OFFSET @Offset
        """;

        var request = connection.QueryAsync<ClassifiedAdListItem>(sql, new
        {
            OwnerId = query.OwnerId,
            PageSize = query.PageSize,
            Offset = Offset(query.Page, query.PageSize)
        });

        return await request;
    }

    public static async Task<ClassifiedAdDetails?> QueryAsync(
        this DbConnection connection,
        GetPublicClassifiedAd query)
    {
        const string sql = """
        SELECT ads."DatabaseId" id, "Price_Amount" price, "Title_Value" title,
        "Description_Value" description, "DisplayName_Value" sellerdisplayname, "Price_Currency_Code" currencycode
        FROM "ClassifiedAds" ads, "UserProfiles" profiles
        WHERE ads."DatabaseId"=@Id AND "OwnerId_Value"=profiles."DatabaseId"
        """;

        var request = connection.QuerySingleOrDefaultAsync<ClassifiedAdDetails>(sql, new
        {
            Id = query.ClassifiedAdId
        });

        return await request;
    }

    private static int Offset(int page, int pageSize) => (page - 1) * pageSize;
}
