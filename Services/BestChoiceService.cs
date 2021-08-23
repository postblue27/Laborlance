using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laborlance_API.Data;
using Laborlance_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Services
{
    public class BestChoiceService
    {
        private readonly DataContext _context;
        public BestChoiceService(DataContext context)
        {
            _context = context;

        }
        public async Task<Dictionary<Proposal, double>> ProposalRating(int orderId)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            var proposals = await _context.Proposals.Where(p => p.OrderId == orderId).ToListAsync();
            // var proposals = order.Proposals;
            Dictionary<Proposal, double> rates = new Dictionary<Proposal, double>();
            IEnumerator<Proposal> i = proposals.GetEnumerator();
            int j = 0;
            while (j < proposals.Count)
            {
                i.MoveNext();
                double rate = i.Current.Worker.Rating /
                    (i.Current.Worker.HourlyWage * 8 + CurrentRentalPriceSum(i.Current.ProposedTools));
                rates.Add(i.Current, rate);
                
                j++;
            }
            // rates.Add(proposals[0], 1);
            return rates;
        }
        public double CurrentRentalPriceSum(ICollection<ToolInProposal> toolsInProposal)
        {
            double sum = 0;
            IEnumerator<ToolInProposal> i = toolsInProposal.GetEnumerator();
            int j = 0;
            while (j < toolsInProposal.Count)
            {
                i.MoveNext();
                sum += i.Current.Tool.RentalPrice;
                
                j++;
            }
            return sum;
        }
    }
}