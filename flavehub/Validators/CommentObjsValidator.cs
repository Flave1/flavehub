using flavehub.Contracts.RequestObjs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Validators
{

    public class AddCommentReqObjValidator : AbstractValidator<AddCommentReqObj>
    {
        public AddCommentReqObjValidator()
        {
            RuleFor(x => x.Commented)
               .NotEmpty();

            RuleFor(x => x.PostId)
                .NotEmpty();

            RuleFor(x => x.Commented)
                 .NotEmpty()
                 .MinimumLength(5);
        }

    }

    public class EditCommentReqObjValidator : AbstractValidator<EditCommentReqObj>
    {
        public EditCommentReqObjValidator()
        {
            RuleFor(x => x.Commented)
               .NotEmpty();

            RuleFor(x => x.PostId)
                .NotEmpty();

            RuleFor(x => x.Commented)
                 .NotEmpty()
                 .MinimumLength(5);
        }
    }
}
