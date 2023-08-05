using AutoMapper;
using IdentityForm.Application.Interfaces.Infrastructures.Repositories;
using IdentityForm.Domain.Entities;
using IdentityForm.Infrastructure.Shared.Interfaces;
using IdentityForm.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityForm.Application.Features.DocumentIdentitys.Commands.AddEdit
{
    public partial class AddEditDocumentIdentityCommand : IRequest<Result<Guid>>
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        public IFormFile FrontDocumentFile { get; set; }
        public IFormFile BackDocumentFile { get; set; }
        public IFormFile CapturedDocumentFile { get; set; }
    }

    internal class AddEditDocumentIdentityCommandHandler : IRequestHandler<AddEditDocumentIdentityCommand, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IS3DataProvider _s3DataProvider;
        private readonly IStringLocalizer<AddEditDocumentIdentityCommandHandler> _localizer;

        public AddEditDocumentIdentityCommandHandler(
            IUnitOfWork<Guid> unitOfWork,
            IMapper mapper,
            IS3DataProvider s3DataProvider,
            IStringLocalizer<AddEditDocumentIdentityCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _s3DataProvider = s3DataProvider;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(AddEditDocumentIdentityCommand command, CancellationToken cancellationToken)
        {
            var documentIdentity = _mapper.Map<DocumentIdentity>(command);
            string path = "documentIdentity/";

            try
            {

                Guid newGuidFront = Guid.NewGuid();
                Stream streamFront = command.FrontDocumentFile.OpenReadStream();
                string keyNameFront = $"{newGuidFront}.jpg";
                string filePathFront = $"{path}{keyNameFront}";
                var resFront = await _s3DataProvider.PostStreamAsync(filePathFront, streamFront);

                Guid newGuidBack = Guid.NewGuid();
                Stream streamBack = command.BackDocumentFile.OpenReadStream();
                string keyNameBack = $"{newGuidBack}.jpg";
                string filePathBack = $"{path}{keyNameBack}";
                var resBack = await _s3DataProvider.PostStreamAsync(filePathBack, streamBack);

                Guid newGuidCaptured = Guid.NewGuid();
                Stream streamCaptured = command.CapturedDocumentFile.OpenReadStream();
                string keyNameCaptured = $"{newGuidCaptured}.jpg";
                string filePathCaptured = $"{path}{keyNameCaptured}";
                var resCaptured = await _s3DataProvider.PostStreamAsync(filePathCaptured, streamCaptured);

                documentIdentity.FirstName = command.FirstName;
                documentIdentity.LastName = command.LastName;
                documentIdentity.Document = command.Document;
                documentIdentity.BackFilePath = filePathBack;
                documentIdentity.FrontFilePath = filePathFront;
                documentIdentity.CapturedImagePath = filePathCaptured;
                documentIdentity.BirthDate = command.BirthDate;
                await _unitOfWork.Repository<DocumentIdentity>().AddAsync(documentIdentity);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(documentIdentity.Id, _localizer["DocumentIdentity criado"]);
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync(ex.Message);
            }
        }
    }
}