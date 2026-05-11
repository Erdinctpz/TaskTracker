using FluentValidation;
using TaskTracker.Application.DTOs;

namespace TaskTracker.Application.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş geçilemez.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir.");

            RuleFor(x => x.Deadline)
                .Must(d => d.Value.Date >= DateTime.Now.Date)
                .When(x => x.Deadline.HasValue)
                .WithMessage("Son tarih geçmiş bir gün olamaz.");
        }
    }
}