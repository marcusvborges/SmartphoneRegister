using SmatphoneRegister.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartphoneRegister.Data.Interface
{
    public interface ISmartPhoneRepository 
    {

        public void CadastrarSmartphones(Smartphones smartphones);
        public List<Smartphones> ListarSmartphones();
        public void AtualizarSmarthpones(Smartphones smartphones);
        public void RemoverSmarthpones(int id);       

    }
}
