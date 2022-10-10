Namespace Library.Core.LDAP
    Public Enum LdapStatusCode
        LDAP_SUCCESS = &H0
        'Sucessful request.
        LDAP_OPERATIONS_ERROR = &H1
        'Intialization of LDAP library failed.
        LDAP_PROTOCOL_ERROR = &H2
        'Protocol error occurred.
        LDAP_TIMELIMIT_EXCEEDED = &H3
        'Time limit has exceeded.
        LDAP_SIZELIMIT_EXCEEDED = &H4
        'Size limit has exceeded.
        LDAP_COMPARE_FALSE = &H5
        'Compare yielded FALSE.
        LDAP_COMPARE_TRUE = &H6
        'Compare yielded TRUE.
        LDAP_AUTH_METHOD_NOT_SUPPORTED = &H7
        'The authentication method is not supported.
        LDAP_STRONG_AUTH_REQUIRED = &H8
        'Strong authentication is required.
        LDAP_REFERRAL_V2 = &H9
        'LDAP version 2 referral.
        LDAP_PARTIAL_RESULTS = &H9
        'Partial results and referrals received.
        LDAP_REFERRAL = &HA
        'Referral occurred.
        LDAP_ADMIN_LIMIT_EXCEEDED = &HB
        'Administration limit on the server has exceeded.
        LDAP_UNAVAILABLE_CRIT_EXTENSION = &HC
        'Critical extension is unavailable.
        LDAP_CONFIDENTIALITY_REQUIRED = &HD
        'Confidentiality is required.
        LDAP_NO_SUCH_ATTRIBUTE = &H10
        'Requested attribute does not exist.
        LDAP_UNDEFINED_TYPE = &H11
        'The type is not defined.  
        LDAP_INAPPROPRIATE_MATCHING = &H12
        'An inappropriate matching occurred. 
        LDAP_CONSTRAINT_VIOLATION = &H13
        'A constraint violation occurred.
        LDAP_ATTRIBUTE_OR_VALUE_EXISTS = &H14
        'The attribute exists or the value has been assigned.
        LDAP_INVALID_SYNTAX = &H15
        'The syntax is invalid.
        LDAP_NO_SUCH_OBJECT = &H20
        'Object does not exist.
        LDAP_ALIAS_PROBLEM = &H21
        'The alias is invalid.
        LDAP_INVALID_DN_SYNTAX = &H22
        'The distinguished name has an invalid syntax.
        LDAP_IS_LEAF = &H23
        'The object is a leaf.
        LDAP_ALIAS_DEREF_PROBLEM = &H24
        'Cannot de-reference the alias.
        LDAP_INAPPROPRIATE_AUTH = &H30
        'Authentication is inappropriate.
        LDAP_INVALID_CREDENTIALS = &H31
        'The supplied credential is invalid.
        LDAP_INSUFFICIENT_RIGHTS = &H32
        'The user has insufficient access rights.
        LDAP_BUSY = &H33
        'The server is busy.
        LDAP_UNAVAILABLE = &H34
        'The server is unavailable.
        LDAP_UNWILLING_TO_PERFORM = &H35
        'The server does not handle directory requests.
        LDAP_LOOP_DETECT = &H36
        'The chain of referrals has looped back to a referring server.
        LDAP_NAMING_VIOLATION = &H40
        'There was a naming violation.
        LDAP_OBJECT_CLASS_VIOLATION = &H41
        'There was an object class violation.
        LDAP_NOT_ALLOWED_ON_NONLEAF = &H42
        'Operation is not allowed on a non-leaf object.
        LDAP_NOT_ALLOWED_ON_RDN = &H43
        'Operation is not allowed on RDN.
        LDAP_ALREADY_EXISTS = &H44
        'The object already exists.
        LDAP_NO_OBJECT_CLASS_MODS = &H45
        'Cannot modify object class.
        LDAP_RESULTS_TOO_LARGE = &H46
        'Results returned are too large.
        LDAP_AFFECTS_MULTIPLE_DSAS = &H47
        'Multiple directory service agents are affected.
        LDAP_OTHER = &H50
        'Unknown error occurred.
        LDAP_SERVER_DOWN = &H51
        'Cannot contact the LDAP server.
        LDAP_LOCAL_ERROR = &H52
        'Local error occurred.
        LDAP_ENCODING_ERROR = &H53
        'Encoding error occurred.
        LDAP_DECODING_ERROR = &H54
        'Decoding error occurred.
        LDAP_TIMEOUT = &H55
        'The search was timed out.
        LDAP_AUTH_UNKNOWN = &H56
        'Unknown authentication error occurred.
        LDAP_FILTER_ERROR = &H57
        'The search filter is incorrect.
        LDAP_USER_CANCELLED = &H58
        'The user has canceled the operation.
        LDAP_PARAM_ERROR = &H59
        'An incorrect parameter was passed to a routine.
        LDAP_NO_MEMORY = &H5A
        'The system is out of memory.
        LDAP_CONNECT_ERROR = &H5B
        'Cannot establish a connection to the server.
        LDAP_NOT_SUPPORTED = &H5C
        'The feature is not supported.
        LDAP_CONTROL_NOT_FOUND = &H5D
        'The ldap function did not find the specified control.
        LDAP_NO_RESULTS_RETURNED = &H5E
        'The feature is not supported.
        LDAP_MORE_RESULTS_TO_RETURN = &H5F
        'Additional results are to be returned.
        LDAP_CLIENT_LOOP = &H60
        'Client loop was detected.
        LDAP_REFERRAL_LIMIT_EXCEEDED = &H61
        'The referral limit was exceeded.
        LDAP_SASL_BIND_IN_PROGRESS = &HE
        'Intermediary bind result for multi-stage binds
    End Enum
End Namespace

