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
        var couponModel = coupon.Adapt<CouponEntity>();
        return couponModel;
    }
    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);

    }
    public override Task<CouponEntity> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        return base.GetDiscount(request, context);
    }
}
    


