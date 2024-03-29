﻿using System;
using System.Windows.Input;

namespace MVVM_Basics.EventAndCommandHandlers;

class RelayCommand<T> : ICommand
{
    private readonly Predicate<T> _canExecute;
    private readonly Action<T> _execute;

    public RelayCommand(Predicate<T> canExecute, Action<T> execute)
    {
        if (execute == null)
            throw new ArgumentNullException("execute");
        _canExecute = canExecute;
        _execute = execute;
    }

    public bool CanExecute(object? parameter)
    {
        try
        {
            return _canExecute == null || _canExecute((T)parameter!);
        }
        catch
        {
            return true;
        }
    }

    public void Execute(object? parameter)
    {
        _execute((T)parameter!);
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; CanExecuteChangedInternal += value; }
        remove { CommandManager.RequerySuggested -= value; CanExecuteChangedInternal -= value; }
    }

    private event EventHandler? CanExecuteChangedInternal;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChangedInternal?.Raise(this);
    }
}
