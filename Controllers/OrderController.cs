using DManage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DManage.Controllers
{
    [Route("[controller][action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DManageContext dmanageContext;

        public OrderController(DManageContext _dmanageContext)
        {
            dmanageContext = _dmanageContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var y = await dmanageContext.Orders.ToListAsync();
            return Ok(y);

        }


        [HttpPost]
        public async Task<IActionResult> GetOrd(Guid OdrID)
        {
            var x = await dmanageContext.Orders.FirstOrDefaultAsync(x => x.OrderId == OdrID);
            return Ok(x);

        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Guid OrderedproductID, string orderType, Guid CustomerID, int quantity)
        {
            Guid newOrderId = Guid.NewGuid();
            Guid newCustomerId = Guid.NewGuid();
            Guid newOrderQuantityID = Guid.NewGuid();
            String OrderStatus = "Pending";






            Order neworder = new Order
            {
                CustomerId = CustomerID,
                OrderId = newOrderId,
                OrderDate = DateTime.Now,
                OrderType = orderType,
                OrderStatus = "Pending"
              /*  Customer = new CustomerTable { CustomerId = CustomerID }*/,
                OrderNavigation = new OrderQuantity { Id = Guid.NewGuid(), ProductId = OrderedproductID, Quantity = quantity }
            };

            dmanageContext.Orders.Add(neworder);
            await dmanageContext.SaveChangesAsync();
            return Ok();





        }




        [HttpPost]
        public async Task<Boolean> VerifyOrder(Guid OdrID)
        {
            Dictionary<Guid, int> availablespace = new Dictionary<Guid, int>();

            Order x = await dmanageContext.Orders.Include(x => x.OrderNavigation).Where(x => x.OrderId == OdrID & x.OrderStatus == "Pending").FirstOrDefaultAsync();
            int orderedQuantity = x.OrderNavigation.Quantity;
            var OrderedproductID = x.OrderNavigation.ProductId;
            var OrderedproductTypeID = dmanageContext.ProductMasters.Where(x => x.ProductId == OrderedproductID).FirstOrDefault().ProductTypeId;


            var SameTypePallateList = dmanageContext.Pallates.Where(x => x.ProductTypeId == OrderedproductTypeID);
            var joinTable = dmanageContext.Pallates.Join(dmanageContext.ProductInventories,
                                                            pall => pall.PallateId,
                                                            prodInv => prodInv.PallateId,
                                                            (pall, prodInv) => new
                                                            {
                                                                pall,
                                                                prodInv


                                                            });

            Boolean SpaceAvailable = false;

            foreach (var item in joinTable)
            {
                availablespace.Add(item.pall.PallateId, item.prodInv.Quantity);
                if (orderedQuantity < item.prodInv.Quantity)
                    SpaceAvailable = true;
                break;

            }


            // return  ;
            //return (JsonConvert.SerializeObject(availablespace));
            return SpaceAvailable;





            // return string($"Order rejected because Pallate capacity is only up to {pallateCapacity} and occupied ")


            //emp1.Join(emp2,
            //            e1 => e1.emp_id,
            //            e2 => e2.emp_id,
            //            (e1, e2) => new {
            //                EmployeeName = e1.emp_name,
            //                EmployeeDepartment = e2.emp_dept
            //            });






        }


        [HttpPost]
        public async Task<IActionResult> AcceptRejectOrder(Guid OdrID)
        {
            Dictionary<Guid, int> availablespace = new Dictionary<Guid, int>();

            Order x = await dmanageContext.Orders.Include(x => x.OrderNavigation).Where(x => x.OrderId == OdrID & x.OrderStatus == "Pending").FirstOrDefaultAsync();
            int orderedQuantity = x.OrderNavigation.Quantity;
            var OrderedproductID = x.OrderNavigation.ProductId;
            var OrderedproductTypeID = dmanageContext.ProductMasters.Where(x => x.ProductId == OrderedproductID).FirstOrDefault().ProductTypeId;


            var SameTypePallateList = dmanageContext.Pallates.Where(x => x.ProductTypeId == OrderedproductTypeID);
            //var joinTable = dmanageContext.Pallates.Join(dmanageContext.ProductInventories,
            //                                                pall => pall.PallateId,
            //                                                prodInv => prodInv.PallateId,

            //                                                (pall, prodInv) => new
            //                                                {
            //                                                    pall,
            //                                                    prodInv
            //                                                }).Where(x => x.pall.ProductTypeId == OrderedproductTypeID).ToList();


            var join = from pall in dmanageContext.Pallates
                       join Pid in dmanageContext.ProductInventories
                       on pall.PallateId equals Pid.PallateId
                       where (pall.ProductTypeId == OrderedproductTypeID)
                       select new { ID= pall.PallateId, quantity= Pid.Quantity };
            var OccupiedSpaceinPallate = join.Sum(x => x.quantity);
            var AvailableSpaceinPallate = SameTypePallateList.Sum(x => x.Capacity);
            

            
            return Ok($"Space occupied  {OccupiedSpaceinPallate} , available space for this product type are {AvailableSpaceinPallate-OccupiedSpaceinPallate} in Node1 ");

            //foreach (var item in joinTable)
            //{
            //    availablespace.Add(item.pall.PallateId, item.prodInv.Quantity);
            //    if (orderedQuantity < item.prodInv.Quantity)
            //        SpaceAvailable = true;
            //    break;

            //}

            // return  ;
            //return (JsonConvert.SerializeObject(availablespace));





        }






    }
}
