using flavehub.Contracts.RequestObjs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Validators
{

    public class AddCategoryReqObjValidator : AbstractValidator<AddCategoryReqObj>
    {
        public AddCategoryReqObjValidator()
        {
            RuleFor(x => x.CategoryName)
               .NotEmpty()
               .MinimumLength(2);

        }
    }
    public class EditCategoryReqObjValidator : AbstractValidator<EditCategoryReqObj>
    {
        public EditCategoryReqObjValidator()
        {
            RuleFor(x => x.CategoryId)
                 .NotEmpty();

            RuleFor(x => x.CategoryName)
                    .NotEmpty()
                    .MinimumLength(2);
        }
    }
}
