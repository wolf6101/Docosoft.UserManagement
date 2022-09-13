namespace Docosoft.UserManagement.Application.SeedWork
{
    public class ResponseDto<TEntityDto> where TEntityDto: IDto
    {
        public ResponseDto(TEntityDto entityDto) : this(entityDto, false, null)
        {
        }
        public ResponseDto(TEntityDto entityDto, bool errorOccured, Exception? error)
        {
            this.EntityDto = entityDto;
            this.ErrorOccured = errorOccured;
            this.Error = error;
        }
        public bool ErrorOccured { get; }
        public Exception? Error { get; }
        public TEntityDto? EntityDto { get; }
        public string? Message { get; set; }
        public bool ResourceUpdated { get; set; }
        public bool ResourceCreated { get; set; }
    }
}