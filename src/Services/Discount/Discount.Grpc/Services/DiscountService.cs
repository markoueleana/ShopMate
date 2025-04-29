using System.Threading;
using Discount.Grpc;
using Discount.Grpc.Entities;
using Grpc.Core;
using Mapster;
using Marten;

namespace Discount.Grpc.Services;

public class DiscountService (IDocumentSession session) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponEntity> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon
        {
          ProductName = request.Coupon.ProductName,
          Amount = (decimal)request.Coupon.Amount

        };
        session.Store(coupon);

        await session.SaveChangesAsync();
        var couponEntity = coupon.Adapt<CouponEntity>();
        return couponEntity;
    }
    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);

    }
    public override async Task<CouponEntity> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await session.LoadAsync<Coupon>(request.ProductName);

        if (coupon is null) coupon = new Coupon { ProductName = "No Discount", Amount = 0 };

        var couponEntity = coupon.Adapt<CouponEntity>();

        return couponEntity;

    }
}
    


