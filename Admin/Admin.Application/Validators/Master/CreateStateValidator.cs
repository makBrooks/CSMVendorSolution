using Admin.Core.Entity.Masters;
using FluentValidation;

namespace Admin.Application.Validators.Master
{
    public class CreateStateValidator : AbstractValidator<State>
    {
        public CreateStateValidator()
        {

            RuleFor(x => x.stateName).NotNull();
            RuleFor(x => x.stateName).Length(0, 10);

            //RuleFor(x => x.stateName).NotEmpty();

            //RuleFor(x => x.stateName)
            //   .NotEmpty()
            //   .WithMessage("")
            //   .MaximumLength(32)
            //   .WithMessage("")
            //   .NotNull();
        }
    }
}
