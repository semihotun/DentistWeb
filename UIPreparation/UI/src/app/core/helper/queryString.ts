export function QueryString(data:object):string
{
    return Object.keys(data)
    .map(key => typeof data[key] != "object" ? key + '=' + data[key] : QueryString(data[key]))
    .join('&');
}

