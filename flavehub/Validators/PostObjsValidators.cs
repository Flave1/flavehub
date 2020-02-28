using flavehub.Contracts.RequestObjs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Validators
{
    public class AddPostReqObjVaildator : AbstractValidator<AddPostReqObj>
    {
        public AddPostReqObjVaildator()
        {
            RuleFor(x => x.PostTitle)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.CategoryId)
                .NotEmpty();

            RuleFor(x => x.DatePosted)
                .NotEmpty();

            RuleFor(x => x.PostBody)
                .MinimumLength(5)
                .NotEmpty();
        }
    }

    public class EditPostReqObjVaildator : AbstractValidator<EditPostReqObj>
    {
        public EditPostReqObjVaildator()
        {
            RuleFor(x => x.PostTitle)
               .NotEmpty()
               .MinimumLength(5);

            RuleFor(x => x.CategoryId)
                .NotEmpty();

            RuleFor(x => x.DatePosted)
                .NotEmpty();

            RuleFor(x => x.PostBody)
                .MinimumLength(5)
                .NotEmpty();

        }
    }
}
