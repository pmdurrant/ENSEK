// ***********************************************************************
// Assembly         : ENSEK,Entities
// Author           : pdurr
// Created          : 04-13-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="MeterReadingDto.cs" company="ENSEK,Entities">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ENSEK.Entities.DTO;

/// <summary>
/// Class MeterReadingDto.
/// </summary>
public class MeterReadingDto
{


    /// <summary>
    /// The iddto
    /// </summary>
    private Int32 _Iddto;
    /// <summary>
    /// The account iddto
    /// </summary>
    private string? _accountIddto;
    /// <summary>
    /// The meter reading date timedto
    /// </summary>
    private string? _meterReadingDateTimedto;
    /// <summary>
    /// The meter read valuedto
    /// </summary>
    private string? _meterReadValuedto;
    /// <summary>
    /// The row nodto
    /// </summary>
    private long _rowNodto;

    /// <summary>
    /// Gets or sets the iddto.
    /// </summary>
    /// <value>The iddto.</value>
    public Int32 Iddto { get => _Iddto; set => _Iddto = value; }


    ////public MeterReadingDto(string? accountId, string meterReadingDateTime, string? meterReadValue)
    ////    {
    ////        AccountIddto = accountId;

    ////        MeterReadingDateTimedto = meterReadingDateTime;
    ////        MeterReadValuedto= meterReadValue;

    ////    }

    /// <summary>
    /// Gets or sets the account iddto.
    /// </summary>
    /// <value>The account iddto.</value>
    public string? AccountIddto { get => _accountIddto; set => _accountIddto = value; }


    /// <summary>
    /// Gets or sets the meter reading date timedto.
    /// </summary>
    /// <value>The meter reading date timedto.</value>
    public string? MeterReadingDateTimedto
    {
        get => _meterReadingDateTimedto;
        set => _meterReadingDateTimedto = value;
    }

    /// <summary>
    /// Gets or sets the meter read valuedto.
    /// </summary>
    /// <value>The meter read valuedto.</value>
    public string? MeterReadValuedto
    {
        get => _meterReadValuedto;
        set => _meterReadValuedto = value;
    }

    /// <summary>
    /// Gets or sets the row nodto.
    /// </summary>
    /// <value>The row nodto.</value>
    public long RowNodto
    {
        get => _rowNodto;
        set => _rowNodto = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MeterReadingDto" /> class.
    /// </summary>
    public MeterReadingDto()
    {
    }

    //public MeterReadingDto(Int64 Id)
    //{
    //    _Iddto = Id;
    //}




}