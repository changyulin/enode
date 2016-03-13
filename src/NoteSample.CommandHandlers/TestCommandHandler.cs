﻿using System;
using System.Threading.Tasks;
using ECommon.IO;
using ENode.Commanding;
using ENode.Infrastructure;
using NoteSample.Commands;
using NoteSample.Domain;

namespace NoteSample.CommandHandlers
{
    public class TestCommandHandler :
        ICommandHandler<TestEventPriorityCommand>,
        ICommandHandler<ChangeMultipleAggregatesCommand>,
        ICommandHandler<ChangeNothingCommand>,
        ICommandHandler<ThrowExceptionCommand>
    {
        public void Handle(ICommandContext context, ChangeNothingCommand command)
        {
            //DO NOTHING
        }
        public void Handle(ICommandContext context, ChangeMultipleAggregatesCommand command)
        {
            context.Get<Note>(command.AggregateRootId1).TestEvents();
            context.Get<Note>(command.AggregateRootId2).TestEvents();
        }
        public void Handle(ICommandContext context, ThrowExceptionCommand command)
        {
            throw new Exception("CommandException");
        }
        public void Handle(ICommandContext context, TestEventPriorityCommand command)
        {
            context.Get<Note>(command.AggregateRootId).TestEvents();
        }
    }

    public class TestCommandHandler1 : ICommandHandler<TwoHandlersCommand>
    {
        public void Handle(ICommandContext context, TwoHandlersCommand command)
        {
            //DO NOTHING
        }
    }
    public class TestCommandHandler2 : ICommandHandler<TwoHandlersCommand>
    {
        public void Handle(ICommandContext context, TwoHandlersCommand command)
        {
            //DO NOTHING
        }
    }


    public class AsyncHandlerCommandHandler : ICommandAsyncHandler<AsyncHandlerCommand>
    {
        public Task<AsyncTaskResult<IApplicationMessage>> HandleAsync(AsyncHandlerCommand command)
        {
            return Task.FromResult(new AsyncTaskResult<IApplicationMessage>(AsyncTaskStatus.Success));
        }
    }
    public class AsyncHandlerCommandHandler2 : ICommandAsyncHandler<AsyncHandlerCommand2>
    {
        public Task<AsyncTaskResult<IApplicationMessage>> HandleAsync(AsyncHandlerCommand2 command)
        {
            return Task.FromResult(new AsyncTaskResult<IApplicationMessage>(AsyncTaskStatus.Success, new TestApplicationMessage(command.AggregateRootId)));
        }
    }
    public class TestApplicationMessage : ApplicationMessage
    {
        public string AggregateRootId { get; set; }

        public TestApplicationMessage() { }
        public TestApplicationMessage(string aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }

        public override string GetRoutingKey()
        {
            return AggregateRootId;
        }
    }
    public class TestCommandAsyncHandler1 : ICommandAsyncHandler<TwoAsyncHandlersCommand>
    {
        public Task<AsyncTaskResult<IApplicationMessage>> HandleAsync(TwoAsyncHandlersCommand command)
        {
            return Task.FromResult(new AsyncTaskResult<IApplicationMessage>(AsyncTaskStatus.Success));
        }
    }
    public class TestCommandAsyncHandler2 : ICommandAsyncHandler<TwoAsyncHandlersCommand>
    {
        public Task<AsyncTaskResult<IApplicationMessage>> HandleAsync(TwoAsyncHandlersCommand command)
        {
            return Task.FromResult(new AsyncTaskResult<IApplicationMessage>(AsyncTaskStatus.Success));
        }
    }
    public class ThrowExceptionAsyncCommandHandler : ICommandAsyncHandler<ThrowExceptionAsyncCommand>
    {
        public Task<AsyncTaskResult<IApplicationMessage>> HandleAsync(ThrowExceptionAsyncCommand command)
        {
            throw new Exception("AsyncCommandException");
        }
    }
}
