// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

using System;
using NExpect.Implementations;

// ReSharper disable IntroduceOptionalParameters.Global

namespace NExpect.MatcherLogic;

/// <summary>
/// Implements IMatcher result, with the added constraint
/// that the result cannot be negated. Useful for when
/// a situation warrants failure both for the positive
/// and negative cases (eg testing a null collection)
/// </summary>
public class EnforcedMatcherResult : MatcherResult
{
    /// <inheritdoc />
    public EnforcedMatcherResult(bool passed) : base(passed)
    {
    }

    /// <inheritdoc />
    public EnforcedMatcherResult(bool passed, string message) : base(passed, message)
    {
    }

    /// <inheritdoc />
    public EnforcedMatcherResult(bool passed, Func<string> messageGenerator) : base(passed, messageGenerator)
    {
    }

    /// <inheritdoc />
    public EnforcedMatcherResult(bool passed, Func<Func<string>> messageGeneratorGenerator) : base(passed, messageGeneratorGenerator)
    {
    }

    /// <inheritdoc />
    public EnforcedMatcherResult(bool passed, Func<string> messageGenerator, Exception localException) : base(passed, messageGenerator, localException)
    {
    }

    /// <inheritdoc />
    public EnforcedMatcherResult(bool passed, Func<string> regularMessageGenerator, Func<string> customMessageGenerator) : base(passed, regularMessageGenerator, customMessageGenerator)
    {
    }

    /// <inheritdoc />
    public EnforcedMatcherResult(bool passed, Func<Func<string>> messageGeneratorGenerator, Exception localException) : base(passed, messageGeneratorGenerator, localException)
    {
    }
}

/// <summary>
/// Implements the IMatcher result, use to contain your
/// matcher result
/// </summary>
public class MatcherResult : IMatcherResult
{
    private Func<string> _messageGenerator;
    private string _message;
    private readonly Func<Func<string>> _messageGeneratorGenerator;

    /// <inheritdoc />
    public bool Passed { get; set; }

    /// <inheritdoc />
    public string Message => _message ??= MessageGenerator?.Invoke();

    /// <inheritdoc />
    public Exception LocalException { get; }

    private Func<string> MessageGenerator =>
        _messageGenerator ??= _messageGeneratorGenerator?.Invoke();

    /// <summary>
    /// Constructor with just passed value -- set message later
    /// </summary>
    /// <param name="passed">Whether or not the matcher passed</param>
    public MatcherResult(bool passed) : this(passed, () => "")
    {
    }

    /// <summary>
    /// Constructor with static message
    /// Consider rather using the constructor with the Func&lt;string&gt;
    /// since that message will only be evaluated late, once, meaning that
    /// if there is any costly work to be done (eg deep .Stringify()) calls,
    /// then that work will only be done when the message is read to display
    /// to the user, instead of every time the assertion is made.
    /// </summary>
    /// <param name="passed">Did the matcher pass?</param>
    /// <param name="message">Message about the pass / fail</param>
    public MatcherResult(
        bool passed,
        string message
    ) : this(passed, () => message)
    {
    }

    /// <summary>
    /// Constructor with message generator
    /// </summary>
    /// <param name="passed">Did the matcher pass?</param>
    /// <param name="messageGenerator">Generator about the pass / fail</param>
    public MatcherResult(
        bool passed,
        Func<string> messageGenerator)
        : this(passed, () => messageGenerator, null)
    {
    }

    /// <summary>
    /// Constructor with message generator generator
    /// </summary>
    /// <param name="passed"></param>
    /// <param name="messageGeneratorGenerator"></param>
    public MatcherResult(
        bool passed,
        Func<Func<string>> messageGeneratorGenerator)
        : this(passed, messageGeneratorGenerator, null)
    {
    }

    /// <summary>
    /// Constructor with message generator generator and local exception
    /// </summary>
    /// <param name="passed"></param>
    /// <param name="messageGenerator"></param>
    /// <param name="localException"></param>
    public MatcherResult(
        bool passed,
        Func<string> messageGenerator,
        Exception localException
    ) : this(passed, () => messageGenerator, localException)
    {
    }

    /// <summary>
    /// Constructor for message generator with two message lambdas
    /// - the regular message generator
    /// - the custom message generator (user-provided)
    /// if the latter produces NULL, it's not echoed back
    /// </summary>
    /// <param name="passed"></param>
    /// <param name="regularMessageGenerator"></param>
    /// <param name="customMessageGenerator"></param>
    public MatcherResult(
        bool passed,
        Func<string> regularMessageGenerator,
        Func<string> customMessageGenerator
    ) : this(passed, MessageHelpers.FinalMessageFor(regularMessageGenerator, customMessageGenerator))
    {
    }

    /// <summary>
    /// Constructor with delayed message generator generator
    /// </summary>
    /// <param name="passed"></param>
    /// <param name="messageGeneratorGenerator"></param>
    /// <param name="localException"></param>
    public MatcherResult(
        bool passed,
        Func<Func<string>> messageGeneratorGenerator,
        Exception localException
    )
    {
        _messageGeneratorGenerator = messageGeneratorGenerator;
        Passed = passed;
        LocalException = localException;
    }
}