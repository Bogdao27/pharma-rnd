using System;

namespace PharmaRnd.API.Contracts;

public record AddBatchRequest(
    string Number, 
    DateOnly manufacturedOn, 
    IFormFileCollection Files);