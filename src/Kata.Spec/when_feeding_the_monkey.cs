using FluentAssertions;
using Machine.Specifications;

namespace Kata.Spec
{
    public class when_airplane_reports_to_control_tower
    {
        Establish _context = () =>
        {
            _airplane = new Airplane();
            _airport = new Airport();
            new ConcreteMediator(_airplane, _airport);
        };

        Because of = () => { _airplane.ReportProximity(); };
        It should_announce_eta = () => { _airport.Eta.Should().Be("Flight #123 will arrive in 30 minutes."); };
        static Airplane _airplane;
        static Airport _airport;
    }

    public class when_airport_welcomes_airplane
    {
        Establish _context = () =>
        {
            _airplane = new Airplane();
            _airport = new Airport();
            new ConcreteMediator(_airplane, _airport);
        };

        Because of = () => {_airport.WelcomePassengers(); };
        It should_announce_welcome_message = () => { _airplane.WelcomeAnnouncement.Should().Be("Aeropuerto Ramon Villeda Morales welcomes you."); };

        It should_confirm_eta = () => { _airport.SpeakerMessage.Should().Be("Flight #123 arrives in 25 minutes."); };
        static Airplane _airplane;
        static Airport _airport;
    }

     class ConcreteMediator : IControlTowerMediator
     {
         Airplane _airplane;
         Airport _airport;
        public ConcreteMediator(Airplane airplane, Airport airport)
        {
            this._airplane = airplane;
            this._airplane.SetMediator(this);
            this._airport = airport;
            this._airport.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "Airplane")
            {
                this._airport.AnnounceEta();
            }

            if (ev == "Welcome")
            {
                this._airplane.ReceiveWelcome();
                this._airport.NotifyAirportPeople();
            }
        }
    }

    public class Airport : BaseComponent
    {
        public string Eta = "";
        public string SpeakerMessage = "";
        public void AnnounceEta()
        {
            Eta = "Flight #123 will arrive in 30 minutes.";
        }

        public void WelcomePassengers()
        {
            this._mediator.Notify(this, "Welcome");
        }

        public void NotifyAirportPeople()
        {
            SpeakerMessage = "Flight #123 arrives in 25 minutes.";
        }
    }

    public class BaseComponent
    {
        protected IControlTowerMediator _mediator;

        public BaseComponent(IControlTowerMediator mediator = null)
        {
            this._mediator = mediator;
        }

        public void SetMediator(IControlTowerMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    public interface IControlTowerMediator
    {
        void Notify(object sender, string ev);
    }

    public class Airplane : BaseComponent
    {
        public string WelcomeAnnouncement = "";
        public void ReportProximity()
        {
            this._mediator.Notify(this, "Airplane");
        }

        public void ReceiveWelcome()
        {
            WelcomeAnnouncement = "Aeropuerto Ramon Villeda Morales welcomes you.";
        }
    }
}