using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly MySqlConnection _dbConnection;

    public CommentsController(MySqlConnection connection)
    {
        _dbConnection = connection;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        var sql = "SELECT Id, User, Text FROM comments ORDER BY Id ASC";

        var comments = await _dbConnection.QueryAsync<Comment>(sql);
        return Ok(comments);
    }


    [HttpPost]
    public async Task<ActionResult<Comment>> PostComment([FromBody] Comment newComment)
    {
        if (newComment == null || string.IsNullOrEmpty(newComment.Text))
        {
            return BadRequest("Invalid comment data.");
        }

        var sql = @"
            INSERT INTO comments (User, Text) 
            VALUES (@User, @Text);
            
            SELECT LAST_INSERT_ID();";


        var newId = await _dbConnection.QuerySingleAsync<int>(sql, new
        {
            newComment.User,
            newComment.Text
        });

        newComment.Id = newId;

        return CreatedAtAction(nameof(GetComments), new { id = newComment.Id }, newComment);
    }
}