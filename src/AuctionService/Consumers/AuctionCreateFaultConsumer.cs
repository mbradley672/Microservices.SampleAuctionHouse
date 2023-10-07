using Contracts.Contracts;
using MassTransit;

namespace AuctionService.Consumers {
    public class AuctionCreateFaultConsumer: IConsumer<Fault<AuctionCreatedContract>> {
        public async Task Consume(ConsumeContext<Fault<AuctionCreatedContract>> context) {
            Console.WriteLine("--> Fault Detected Consuming faulty creation");
            var exception = context.Message.Exceptions.First();
            if (exception.ExceptionType == "System.ArgumentException")
            {
                
            }
            else
            {
                //TODO: Expand upon error catching.
                Console.WriteLine("Not an argument Exception -- Update error dashboard here");
            }
        }
    }
}
