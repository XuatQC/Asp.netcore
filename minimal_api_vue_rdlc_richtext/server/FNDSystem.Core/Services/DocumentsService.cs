using FNDSystem.Core.ServicesContracts;
using Microsoft.Office.Interop.Word;
using System.Text;

namespace FNDSystem.Core.Services;
public class DocumentsService : GeneticService, IDocumentsService
{
    public DocumentsService()
    {
    }

    /// <summary>
    /// �h�L�������g�ǂݍ��݂����e���擾����
    /// </summary>
    /// <param name="filePath">�t�@�C���p�X</param>
    /// <returns>�t�@�C���̓��e</returns>
    public StringBuilder GetWordFileContent(string filePath)
    {

        Application wordApp = new Application();

        //�h�L�������g�N���X�̃I�u�W�F�N�g���쐬����
        Document doc;

        //�A�b�v���[�h���ꂽ�t�@�C���̃t���p�X���擾���܂�
        dynamic FilePath = Path.GetFullPath(filePath);

        //�I�v�V�����́i�������Ă���j�p�����[�^�� API �ɓn���܂�
        dynamic NA = System.Type.Missing;

        //Word�t�@�C���������J��
        doc = wordApp.Documents.Open
                      (ref FilePath, ref NA, ref NA, ref NA, ref NA,
                       ref NA, ref NA, ref NA, ref NA,
                       ref NA, ref NA, ref NA, ref NA,
                       ref NA, ref NA, ref NA);

        //������r���_�[�N���X�̃I�u�W�F�N�g���쐬����
        StringBuilder fileContent = new StringBuilder();

        for (int Line = 0; Line < doc.Paragraphs.Count; Line++)
        {
            string Filedata = doc.Paragraphs[Line + 1].Range.Text.Trim();

            if (Filedata != string.Empty)
            {
                //Word �t�@�C���̃f�[�^�� stringbuilder �ɒǉ�����
                fileContent.AppendLine(Filedata);
            }
        }

        //�h�L�������g�I�u�W�F�N�g�����
        ((_Document)doc).Close();

        //�A�v���P�[�V�����I�u�W�F�N�g���I�����ăv���Z�X���I�����܂�
        ((_Application)wordApp).Quit();

        return fileContent;
    }
}