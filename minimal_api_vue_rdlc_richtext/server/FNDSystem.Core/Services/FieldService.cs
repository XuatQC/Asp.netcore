using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Extensions;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class FieldService : GeneticService, IFieldService
{
    private readonly IUnitOfWork unitOfWork;

    public FieldService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region 他の条件により分野一覧を取得する
    /// <summary>
    /// 他の条件により分野一覧を取得する
    /// </summary>
    /// <param name="findFieldDto">条件検索</param>
    /// <returns>分野マスタリスト</returns>
    public IEnumerable<Field> FindMany(FindFieldRequestDto? findFieldDto)
    {
        Expression<Func<Field, bool>>? whereExpression = null;
        if (findFieldDto == null)
        {
            if (!string.IsNullOrEmpty(findFieldDto?.PlantName))
            {
                whereExpression = fieldItem => fieldItem.PlantName == findFieldDto.PlantName;
            }

            if (!string.IsNullOrEmpty(findFieldDto?.FieldsName))
            {
                Expression<Func<Field, bool>> byFieldsName = fieldItem => fieldItem.FieldsName == findFieldDto.FieldsName;
                if (whereExpression != null)
                {
                    whereExpression = whereExpression.AndEx<Field>(byFieldsName);
                }
                else
                {
                    whereExpression = fieldItem => fieldItem.FieldsName == findFieldDto.FieldsName;
                }
            }

            if (!string.IsNullOrEmpty(findFieldDto?.Fields))
            {
                Expression<Func<Field, bool>> byFields = fieldItem => fieldItem.Fields == findFieldDto.Fields;
                if (whereExpression != null)
                {
                    whereExpression = whereExpression.AndEx<Field>(byFields);
                }
                else
                {
                    whereExpression = fieldItem => fieldItem.Fields == findFieldDto.Fields;
                }
            }
        }
        var fieldList = unitOfWork.FieldRepository.GetAll(whereExpression, 0, null);

        return fieldList;
    }
    #endregion

    #region 分野一覧取得
    /// <summary>
    /// 分野一覧取得
    /// </summary>
    /// <param name="findFieldDto">条件検索</param>
    /// <returns>分野マスタリスト</returns>
    public IEnumerable<FieldResponseDto>? GetFieldList(FindFieldRequestDto findFieldDto)
    {
        IEnumerable<FieldResponseDto>? objItem = unitOfWork.FieldRepository.GetFieldList(findFieldDto);

        return objItem;
    }
    #endregion

    #region 分野11件取得
    /// <summary>
    /// 分野11件取得
    /// </summary>
    /// <param name="findFieldDto">条件検索</param>
    /// <returns> 分野11</returns>
    public IEnumerable<Field>? Get_11Records_Field(FindFieldRequestDto findFieldDto)
    {
        IEnumerable<Field>? objItem = unitOfWork.FieldRepository.Get_11Records_Field(findFieldDto);

        return objItem;
    }
    #endregion
}