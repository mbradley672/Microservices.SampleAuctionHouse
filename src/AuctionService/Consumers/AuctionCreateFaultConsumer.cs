using Contracts.Contracts;
using MassTransit;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace AuctionService.Consumers {
    public abstract class AuctionCreateFaultConsumer : IConsumer<Fault<AuctionCreated>> {
        public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context) {
            Console.WriteLine("--> Fault Detected Consuming faulty creation");
            var exception = context.Message.Exceptions.First();
            if (exception.ExceptionType == "System.ArgumentException")
            { }
            else
            {
                //TODO: Expand upon error catching.
                Console.WriteLine("Not an argument Exception -- Update error dashboard here");
            }
        }
    }
}