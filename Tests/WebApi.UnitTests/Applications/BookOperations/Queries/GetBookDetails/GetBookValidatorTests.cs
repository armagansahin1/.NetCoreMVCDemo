using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Queries.GetBookDetails;
using Xunit;

namespace Applications.BookOperations.Queries.GetBookDetails
{
    public class GetBookValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenGivenBookIdLessOrEqualThanZero_Validator_ShouldBeReturnError(int id)
        {
            GetBookDetailsQuery query = new GetBookDetailsQuery(null,null);
            query.Id = id;
            GetBookDetailsValidator validator = new GetBookDetailsValidator();
            

            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidBookIdGiven_Validator_ShouldNotBeReturnError()
        {
            GetBookDetailsQuery query = new GetBookDetailsQuery(null,null);
            query.Id = 1;
            GetBookDetailsValidator validator = new GetBookDetailsValidator();

            var result = validator.Validate(query);

            result.Errors.Count.Should().Equals(0);
        }
    }
}