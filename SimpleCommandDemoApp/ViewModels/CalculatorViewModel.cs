using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SimpleCommandDemoApp.ViewModels
{
    //TODO 创建关于错误的snippet 方便快捷写代码
    //TODO 有错误时，改变命令的可执行性状态
    public class CalculatorViewModel : ViewModelBase, IDataErrorInfo
    {
        private int? _second;
        private int? _first;

        private readonly CalculatorValidator _validator;

        /// <summary>
        /// 第一个输入值
        /// </summary>
        public int? First
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
                //命令变化
                AddCommand.RaiseCanExecuteChanged();
                SubstractCommand.RaiseCanExecuteChanged();
                MultiplyCommand.RaiseCanExecuteChanged();
                DivideCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// 第二个输入值
        /// </summary>
        public int? Second
        {
            get
            {
                return _second;
            }
            set
            {
                _second = value;

                //命令变化
                AddCommand.RaiseCanExecuteChanged();
                SubstractCommand.RaiseCanExecuteChanged();
                MultiplyCommand.RaiseCanExecuteChanged();
                DivideCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// 结果
        /// </summary>
        public double? Result { get; set; }

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand SubstractCommand { get; private set; }
        public RelayCommand MultiplyCommand { get; private set; }
        public RelayCommand DivideCommand { get; private set; }

        public CalculatorViewModel()
        {
            _validator = new CalculatorValidator();

            AddCommand = new RelayCommand(AddCommondExecute, AddCommandCanExecute);
            SubstractCommand = new RelayCommand(SubstractCommandExecute, SubstractCommandCanExecute);
            MultiplyCommand = new RelayCommand(MultiplyCommandExecute, MultiplyCommandCanExecute);
            DivideCommand = new RelayCommand(DivideCommandExecute, DivideCommandCanExecute);
        }

        private bool DivideCommandCanExecute()
        {
            return IsFirstAndSecondBothNotNull()
                   && Second != null && (int)Second != 0;
        }

        private void DivideCommandExecute()
        {
            Result = First * 1.0 / Second;
        }

        private bool MultiplyCommandCanExecute()
        {
            return IsFirstAndSecondBothNotNull();
        }

        private void MultiplyCommandExecute()
        {
            Result = First * Second;
        }

        private bool SubstractCommandCanExecute()
        {
            return IsFirstAndSecondBothNotNull();
        }

        private bool IsFirstAndSecondBothNotNull()
        {
            var errors = _validator.Validate(this).Errors;

            return errors != null && errors.Count == 0;
        }

        private void SubstractCommandExecute()
        {
            Result = First - Second;
        }

        private bool AddCommandCanExecute()
        {
            return IsFirstAndSecondBothNotNull();
        }

        private void AddCommondExecute()
        {
            Result = First + Second;
        }

        public string this[string columnName]
        {
            get
            {
                var firstOrDefault = _validator
                    .Validate(this)
                    .Errors.FirstOrDefault(v => v.PropertyName == columnName);

                if (firstOrDefault != null)
                {
                    return _validator != null ? firstOrDefault.ErrorMessage : "";
                }

                return "";
            }
        }

        public string Error
        {
            get
            {
                if (_validator != null)
                {
                    var results = _validator.Validate(this);
                    if (results != null && results.Errors.Any())
                    {
                        var errors = string
                            .Join(
                            Environment.NewLine,
                            results.Errors.Select(x => x.ErrorMessage).ToArray());

                        return errors;
                    }
                }
                return string.Empty;
            }
        }
    }
}